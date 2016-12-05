namespace Sitecore.FakeDb.Tests.Links
{
    using System;
    using System.Web;
    using FluentAssertions;
    using NSubstitute;
    using Ploeh.AutoFixture.Xunit2;
    using Sitecore.Abstractions;
    using Sitecore.Common;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb.Links;
    using Sitecore.Links;
    using Sitecore.Sites;
    using Sitecore.Web;
    using Xunit;

    public class SwitchingLinkManagerTest
    {
        [Theory, AutoData]
        public void SutIsLinkManager(SwitchingLinkManager sut)
        {
            sut.Should().BeAssignableTo<BaseLinkManager>();
        }

        [Theory, AutoData]
        public void AddAspxExtensionWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            string text)
        {
            sut.Invoking(s =>
            {
                var r = s.AddAspxExtension;
            })
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void AddAspxExtensionWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            string text,
            BaseLinkManager current,
            bool expected)
        {
            current.AddAspxExtension.Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.AddAspxExtension
                    .Should().Be(expected);
            }
        }

        [Theory, AutoData]
        public void AlwaysIncludeServerUrlWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            string text)
        {
            sut.Invoking(s =>
            {
                var r = s.AlwaysIncludeServerUrl;
            })
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void AlwaysIncludeServerUrlWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            string text,
            BaseLinkManager current,
            bool expected)
        {
            current.AlwaysIncludeServerUrl.Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.AlwaysIncludeServerUrl
                    .Should().Be(expected);
            }
        }

        [Theory, AutoData]
        public void LanguageEmbeddingWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            string text)
        {
            sut.Invoking(s =>
            {
                var r = s.LanguageEmbedding;
            })
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory]
        [InlineDefaultSubstituteAutoData(LanguageEmbedding.Always)]
        [InlineDefaultSubstituteAutoData(LanguageEmbedding.AsNeeded)]
        [InlineDefaultSubstituteAutoData(LanguageEmbedding.Never)]
        public void LanguageEmbeddingWithSwitchedManagerReturnsProperValue(
            LanguageEmbedding expected,
            SwitchingLinkManager sut,
            string text,
            BaseLinkManager current)
        {
            current.LanguageEmbedding.Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.LanguageEmbedding
                    .Should().Be(expected);
            }
        }

        [Theory, AutoData]
        public void LanguageLocationWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            string text)
        {
            sut.Invoking(s =>
            {
                var r = s.LanguageLocation;
            })
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory]
        [InlineDefaultSubstituteAutoData(LanguageLocation.FilePath)]
        [InlineDefaultSubstituteAutoData(LanguageLocation.QueryString)]
        public void LanguageLocationWithSwitchedManagerReturnsProperValue(
            LanguageLocation expected,
            SwitchingLinkManager sut,
            string text,
            BaseLinkManager current)
        {
            current.LanguageLocation.Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.LanguageLocation
                    .Should().Be(expected);
            }
        }

        [Theory, AutoData]
        public void LowercaseUrlsWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            string text)
        {
            sut.Invoking(s =>
            {
                var r = s.LowercaseUrls;
            })
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void LowercaseUrlsWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            string text,
            BaseLinkManager current,
            bool expected)
        {
            current.LowercaseUrls.Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.LowercaseUrls
                    .Should().Be(expected);
            }
        }

        [Theory, AutoData]
        public void ShortenUrlsWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            string text)
        {
            sut.Invoking(s =>
            {
                var r = s.ShortenUrls;
            })
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void ShortenUrlsWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            string text,
            BaseLinkManager current,
            bool expected)
        {
            current.ShortenUrls.Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.ShortenUrls
                    .Should().Be(expected);
            }
        }

        [Theory, AutoData]
        public void UseDisplayNameWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            string text)
        {
            sut.Invoking(s =>
            {
                var r = s.UseDisplayName;
            })
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void UseDisplayNameWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            string text,
            BaseLinkManager current,
            bool expected)
        {
            current.UseDisplayName.Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.UseDisplayName
                    .Should().Be(expected);
            }
        }

        [Theory, AutoData]
        public void ExpandDynamicLinksByTextWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            string text)
        {
            sut.Invoking(s => s.ExpandDynamicLinks(text))
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void ExpandDynamicLinksByTextWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            string text,
            BaseLinkManager current,
            string expected)
        {
            current.ExpandDynamicLinks(text).Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.ExpandDynamicLinks(text)
                    .Should().Be(expected);
            }
        }

        [Theory, AutoData]
        public void ExpandDynamicLinksByTextAndResolveStatesWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            string text,
            bool resolveStates)
        {
            sut.Invoking(s => s.ExpandDynamicLinks(text, resolveStates))
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void ExpandDynamicLinksByTextAndResolveStatesWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            string text,
            bool resolveStates,
            BaseLinkManager current,
            string expected)
        {
            current.ExpandDynamicLinks(text, resolveStates).Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.ExpandDynamicLinks(text, resolveStates)
                    .Should().Be(expected);
            }
        }

        [Theory, AutoData]
        public void GetDefaultUrlOptionsWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut)
        {
            sut.Invoking(s => s.GetDefaultUrlOptions())
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void GetDefaultUrlOptionsSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            BaseLinkManager current,
            [NoAutoProperties] UrlOptions expected)
        {
            current.GetDefaultUrlOptions().Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.GetDefaultUrlOptions()
                    .Should().Be(expected);
            }
        }

        [Theory, DefaultAutoData]
        public void GetDynamicUrlByItemWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            Item item)
        {
            sut.Invoking(s => s.GetDynamicUrl(item))
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void GetDynamicUrlByItemWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            BaseLinkManager current,
            Item item,
            string expected)
        {
            current.GetDynamicUrl(item).Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.GetDynamicUrl(item)
                    .Should().Be(expected);
            }
        }

        [Theory, DefaultAutoData]
        public void GetDynamicUrlByItemAndOptionsWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            Item item,
            LinkUrlOptions options)
        {
            sut.Invoking(s => s.GetDynamicUrl(item, options))
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void GetDynamicUrlByItemAndOptionsWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            BaseLinkManager current,
            Item item,
            LinkUrlOptions options,
            string expected)
        {
            current.GetDynamicUrl(item, options).Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.GetDynamicUrl(item, options)
                    .Should().Be(expected);
            }
        }

        [Theory, DefaultAutoData]
        public void GetItemUrlByItemWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            Item item)
        {
            sut.Invoking(s => s.GetItemUrl(item))
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void GetItemUrlByItemWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            BaseLinkManager current,
            Item item,
            string expected)
        {
            current.GetItemUrl(item).Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.GetItemUrl(item)
                    .Should().Be(expected);
            }
        }

        [Theory, DefaultAutoData]
        public void GetItemUrlByItemAndOptionsWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            Item item,
            [NoAutoProperties] UrlOptions options)
        {
            sut.Invoking(s => s.GetItemUrl(item, options))
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void GetItemUrlByItemAndOptionsWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            BaseLinkManager current,
            Item item,
            [NoAutoProperties] UrlOptions options,
            string expected)
        {
            current.GetItemUrl(item, options).Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.GetItemUrl(item, options)
                    .Should().Be(expected);
            }
        }

        [Theory, AutoData]
        public void IsDynamicLinkWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            string text)
        {
            sut.Invoking(s => s.IsDynamicLink(text))
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void IsDynamicLinkWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            string text,
            BaseLinkManager current,
            bool expected)
        {
            current.IsDynamicLink(text).Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.IsDynamicLink(text)
                    .Should().Be(expected);
            }
        }

        [Theory, AutoData]
        public void ParseDynamicLinkWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            string text)
        {
            sut.Invoking(s => s.ParseDynamicLink(text))
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory, DefaultSubstituteAutoData]
        public void ParseDynamicLinkWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            string text,
            BaseLinkManager current,
            DynamicLink expected)
        {
            current.ParseDynamicLink(text).Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.ParseDynamicLink(text)
                    .Should().Be(expected);
            }
        }

        [Theory(Skip = "Configure AF to create HttpRequest specimens."), AutoData]
        public void ParseRequestUrlWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            HttpRequest request)
        {
            sut.Invoking(s => s.ParseRequestUrl(request))
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory(Skip = "Configure AF to create HttpRequest specimens."), DefaultSubstituteAutoData]
        public void ParseRequestUrlWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            BaseLinkManager current,
            HttpRequest request,
            RequestUrl expected)
        {
            current.ParseRequestUrl(request).Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.ParseRequestUrl(request)
                    .Should().Be(expected);
            }
        }

        [Theory, DefaultAutoData]
        public void ResolveTargetSiteWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            Item item)
        {
            sut.Invoking(s => s.ResolveTargetSite(item))
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory(Skip = "Configure AF to create SiteInfo specimens."), DefaultSubstituteAutoData]
        public void ResolveTargetSiteWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            BaseLinkManager current,
            Item item,
            SiteInfo expected)
        {
            current.ResolveTargetSite(item).Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.ResolveTargetSite(item)
                    .Should().Be(expected);
            }
        }

        [Theory, DefaultAutoData]
        public void GetPreviewSiteContextWithoutSwitchedManagerThrows(
            SwitchingLinkManager sut,
            Item item)
        {
            sut.Invoking(s => s.GetPreviewSiteContext(item))
                .ShouldThrow<InvalidOperationException>()
                .WithMessage("SwitchingLinkManager has not been properly configured. " +
                             "Probably you forgot to set the Current manager using " +
                             "`Switcher<BaseLinkManager>`.");
        }

        [Theory(Skip = "Configure AF to create SiteContext specimens."), DefaultSubstituteAutoData]
        public void GetPreviewSiteContextWithSwitchedManagerReturnsProperValue(
            SwitchingLinkManager sut,
            BaseLinkManager current,
            Item item,
            SiteContext expected)
        {
            current.GetPreviewSiteContext(item).Returns(expected);
            using (new Switcher<BaseLinkManager>(current))
            {
                sut.GetPreviewSiteContext(item)
                    .Should().Be(expected);
            }
        }
    }
}