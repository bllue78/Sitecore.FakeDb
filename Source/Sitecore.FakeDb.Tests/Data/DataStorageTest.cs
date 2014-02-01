﻿namespace Sitecore.FakeDb.Tests.Data
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb.Data;
  using Sitecore.FakeDb.Data.Engines;
  using Xunit;
  using Xunit.Extensions;

  public class DataStorageTest
  {
    private readonly Database database;

    private readonly DataStorage dataStorage;

    private const string ItemIdsRootId = "{11111111-1111-1111-1111-111111111111}";

    private const string ItemIdsContentRoot = "{0DE95AE4-41AB-4D01-9EB0-67441B7C2450}";

    private const string ItemIdsTemplateRoot = "{3C1715FE-6A13-4FCF-845F-DE308BA9741D}";

    private const string TemplateIdsTemplate = "{AB86861A-6030-46C5-B394-E8F99E8B87DB}";

    private const string ItemIdsTemplateSection = "{E269FBB5-3750-427A-9149-7AA950B49301}";

    private const string ItemIdsTemplateField = "{455A3E98-A627-4B40-8035-E683A0331AC7}";

    private const string TemplateIdsBranch = "{35E75C72-4985-4E09-88C3-0EAC6CD1E64F}";

    private const string RootParentId = "{00000000-0000-0000-0000-000000000000}";

    public DataStorageTest()
    {
      this.database = new FakeDatabase("master");
      this.dataStorage = new DataStorage();
      this.dataStorage.SetDatabase(this.database);
    }

    [Theory]
    [InlineData(ItemIdsRootId, "sitecore", "{C6576836-910C-4A3D-BA03-C277DBD3B827}", RootParentId, "/sitecore")]
    [InlineData(ItemIdsContentRoot, "content", "{E3E2D58C-DF95-4230-ADC9-279924CECE84}", ItemIdsRootId, "/sitecore/content")]
    [InlineData(ItemIdsTemplateRoot, "templates", "{E3E2D58C-DF95-4230-ADC9-279924CECE84}", ItemIdsRootId, "/sitecore/templates")]
    [InlineData(TemplateIdsTemplate, "Template", TemplateIdsTemplate, ItemIdsTemplateRoot, "/sitecore/templates/template")]
    [InlineData(ItemIdsTemplateSection, "Template section", TemplateIdsTemplate, ItemIdsTemplateRoot, "/sitecore/templates/template section")]
    [InlineData(ItemIdsTemplateField, "Template field", TemplateIdsTemplate, ItemIdsTemplateRoot, "/sitecore/templates/template field")]
    [InlineData(TemplateIdsBranch, "Branch", TemplateIdsTemplate, ItemIdsTemplateRoot, "/sitecore/templates/branch")]
    public void ShouldInitializeDefaultFakeItems(string itemId, string itemName, string templateId, string parentId, string fullPath)
    {
      // assert
      this.dataStorage.FakeItems[ID.Parse(itemId)].ID.ToString().Should().Be(itemId);
      this.dataStorage.FakeItems[ID.Parse(itemId)].Name.Should().Be(itemName);
      this.dataStorage.FakeItems[ID.Parse(itemId)].TemplateID.ToString().Should().Be(templateId);
      this.dataStorage.FakeItems[ID.Parse(itemId)].ParentID.ToString().Should().Be(parentId);
      this.dataStorage.FakeItems[ID.Parse(itemId)].FullPath.Should().Be(fullPath);
    }


    [Fact]
    public void ShouldCreateDefaultFakeTemplate()
    {
      this.dataStorage.FakeTemplates[TemplateIDs.Template].Should().BeEquivalentTo(new DbTemplate("Template", TemplateIDs.Template));
    }

    [Fact]
    public void ShouldResetItemsToDefault()
    {
      // arrange
      var itemId = ID.NewID;

      this.dataStorage.FakeItems.Add(itemId, new DbItem("new item"));

      // act
      this.dataStorage.Reset();

      // assert
      this.dataStorage.FakeItems.ContainsKey(itemId).Should().BeFalse();
      this.dataStorage.FakeItems.ContainsKey(ItemIDs.RootID).Should().BeTrue();
      this.dataStorage.FakeItems.ContainsKey(ItemIDs.ContentRoot).Should().BeTrue();
      this.dataStorage.FakeItems.ContainsKey(ItemIDs.TemplateRoot).Should().BeTrue();
    }

    [Fact]
    public void ShouldResetTemplates()
    {
      // arrange
      this.dataStorage.FakeTemplates.Add(ID.NewID, new DbTemplate("some template", ID.NewID));

      // act
      this.dataStorage.Reset();

      // assert
      // TODO:[Minor] Define 'default' templates.
      this.dataStorage.FakeTemplates.Single().Key.Should().Be(TemplateIDs.Template);
    }

    [Fact]
    public void ShouldGetExistingItem()
    {
      // act & assert
      this.dataStorage.GetFakeItem(ItemIDs.ContentRoot).Should().NotBeNull();
      this.dataStorage.GetFakeItem(ItemIDs.ContentRoot).Should().BeOfType<DbItem>();

      this.dataStorage.GetSitecoreItem(ItemIDs.ContentRoot).Should().NotBeNull();
      this.dataStorage.GetSitecoreItem(ItemIDs.ContentRoot).Should().BeOfType<Item>();
    }

    [Fact]
    public void ShouldGetNullIdNoItemPresent()
    {
      // act & assert
      this.dataStorage.GetFakeItem(ID.NewID).Should().BeNull();
      this.dataStorage.GetSitecoreItem(ID.NewID).Should().BeNull();
    }

    [Fact]
    public void ShouldGetFieldListByTemplateId()
    {
      // arrange
      var template = new DbTemplate { "Field 1", "Field 2" };
      var fieldId1 = template.Fields["Field 1"];
      var fieldId2 = template.Fields["Field 2"];

      this.dataStorage.FakeTemplates.Add(template.ID, template);

      // act
      var fieldList = this.dataStorage.GetFieldList(template.ID);

      // assert
      fieldList.Count.Should().Be(2);
      fieldList[fieldId1].Should().BeEmpty();
      fieldList[fieldId2].Should().BeEmpty();
    }

    [Fact]
    public void ShouldThrowExceptionIfNoTemplateFound()
    {
      // arrange
      var missingTemplateId = new ID("{C4520D42-33CA-48C7-972D-6CEE1BC4B9A6}");

      // act
      Action a = () => this.dataStorage.GetFieldList(missingTemplateId);

      // assert
      a.ShouldThrow<InvalidOperationException>().WithMessage("Template \'{C4520D42-33CA-48C7-972D-6CEE1BC4B9A6}\' not found.");
    }
  }
}