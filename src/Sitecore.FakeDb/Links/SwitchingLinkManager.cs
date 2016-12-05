namespace Sitecore.FakeDb.Links
{
    using System;
    using System.Web;
    using Sitecore.Abstractions;
    using Sitecore.Common;
    using Sitecore.Data.Items;
    using Sitecore.Links;
    using Sitecore.Sites;
    using Sitecore.Web;

    public class SwitchingLinkManager : BaseLinkManager
    {
        public override bool AddAspxExtension
        {
            get
            {
                var current = GetCurrentLinkManager();
                return current.AddAspxExtension;
            }
        }

        public override bool AlwaysIncludeServerUrl
        {
            get
            {
                var current = GetCurrentLinkManager();
                return current.AlwaysIncludeServerUrl;
            }
        }

        public override LanguageEmbedding LanguageEmbedding
        {
            get
            {
                var current = GetCurrentLinkManager();
                return current.LanguageEmbedding;
            }
        }

        public override LanguageLocation LanguageLocation
        {
            get
            {
                var current = GetCurrentLinkManager();
                return current.LanguageLocation;
            }
        }

        public override bool LowercaseUrls
        {
            get
            {
                var current = GetCurrentLinkManager();
                return current.LowercaseUrls;
            }
        }

        public override bool ShortenUrls
        {
            get
            {
                var current = GetCurrentLinkManager();
                return current.ShortenUrls;
            }
        }

        public override bool UseDisplayName
        {
            get
            {
                var current = GetCurrentLinkManager();
                return current.UseDisplayName;
            }
        }

        public override string ExpandDynamicLinks(string text)
        {
            var current = GetCurrentLinkManager();
            return current.ExpandDynamicLinks(text);
        }

        public override string ExpandDynamicLinks(string text, bool resolveSites)
        {
            var current = GetCurrentLinkManager();
            return current.ExpandDynamicLinks(text, resolveSites);
        }

        public override UrlOptions GetDefaultUrlOptions()
        {
            var current = GetCurrentLinkManager();
            return current.GetDefaultUrlOptions();
        }

        public override string GetDynamicUrl(Item item)
        {
            var current = GetCurrentLinkManager();
            return current.GetDynamicUrl(item);
        }

        public override string GetDynamicUrl(Item item, LinkUrlOptions options)
        {
            var current = GetCurrentLinkManager();
            return current.GetDynamicUrl(item, options);
        }

        public override string GetItemUrl(Item item)
        {
            var current = GetCurrentLinkManager();
            return current.GetItemUrl(item);
        }

        public override string GetItemUrl(Item item, UrlOptions options)
        {
            var current = GetCurrentLinkManager();
            return current.GetItemUrl(item, options);
        }

        public override bool IsDynamicLink(string linkText)
        {
            var current = GetCurrentLinkManager();
            return current.IsDynamicLink(linkText);
        }

        public override DynamicLink ParseDynamicLink(string linkText)
        {
            var current = GetCurrentLinkManager();
            return current.ParseDynamicLink(linkText);
        }

        public override RequestUrl ParseRequestUrl(HttpRequest request)
        {
            var current = GetCurrentLinkManager();
            return current.ParseRequestUrl(request);
        }

        public override SiteInfo ResolveTargetSite(Item item)
        {
            var current = GetCurrentLinkManager();
            return current.ResolveTargetSite(item);
        }

        public override SiteContext GetPreviewSiteContext(Item item)
        {
            var current = GetCurrentLinkManager();
            return current.GetPreviewSiteContext(item);
        }

        private static BaseLinkManager GetCurrentLinkManager()
        {
            var currentManager = Switcher<BaseLinkManager>.CurrentValue;
            if (currentManager == null)
            {
                throw new InvalidOperationException("SwitchingLinkManager has not been properly configured. " +
                                                    "Probably you forgot to set the Current manager using " +
                                                    "`Switcher<BaseLinkManager>`.");
            }

            return currentManager;
        }
    }
}