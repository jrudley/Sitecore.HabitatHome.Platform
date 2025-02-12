﻿using System.Collections.Generic;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Sitecore.HabitatHome.Feature.Components.Models;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;

namespace Sitecore.HabitatHome.Feature.Components.Controllers
{
    public class ComponentController : Controller
    {
        private Component component;
        private Item dataSourceItem;

        protected Component Component
        {
            get
            {
                if (component == null)
                {
                    var item = DataSourceItem ?? Context.Item;
                    component = new Component {Item = item};
                }

                return component;
            }
        }

        protected Item DataSourceItem
        {
            get
            {
                var dataSource = RenderingContext.CurrentOrNull.Rendering.DataSource;
                if (dataSourceItem == null &&
                    !string.IsNullOrEmpty(dataSource))
                    dataSourceItem = Context.Database.GetItem(dataSource);
                return dataSourceItem;
            }
        }

        public ViewResult Carousel()
        {
            var model = new CarouselModel {Item = Component.Item};
            model.Slides = model.GetChildren<CarouselSlideModel>();
            return View(model);
        }

        public ViewResult CardContainer()
        {
            return View(Component);
        }

        public ViewResult Metadata()
        {
            var metadata = new Metadata {Item = Context.Item};
            return View(metadata);
        }

        public ViewResult Breadcrumb()
        {
            var items = new List<BreadcrumbItem>();

            foreach (var item in Context.Item.Axes.GetAncestors())
            {
                var navigationTitle = item.Fields[Templates.NavigationBase.Fields.NavigationTitle].ToString();
                if (!string.IsNullOrEmpty(navigationTitle))
                {
                    var breadcrumbItem = new BreadcrumbItem
                    {
                        Title = item.Fields[Templates.NavigationBase.Fields.NavigationTitle].ToString(),
                        Url = LinkManager.GetItemUrl(item)
                    };

                    items.Add(breadcrumbItem);
                }
            }

            var currentContextItem = Context.Item;
            var currentBreadcrumbItem = new BreadcrumbItem
            {
                Title = currentContextItem.Fields[Templates.NavigationBase.Fields.NavigationTitle].ToString(),
                Active = true
            };
            items.Add(currentBreadcrumbItem);

            return View("~/Areas/Components/Views/Component/Breadcrumb.cshtml", items);
        }

        public ViewResult OurTeam()
        {
            return View(Component);
        }

        public ViewResult Hero()
        {
            return View(Component);
        }

        public ViewResult Navbar()
        {
            var component = Component;
            if (DataSourceItem == null)
                component = component?.Site?.Home;
            return View(component);
        }

        public ViewResult PageContent()
        {
            return View(Component);
        }

        public ViewResult PageTitle()
        {
            return View(Component);
        }

        public ViewResult CenteredHeading()
        {
            return View(Component);
        }

        public ViewResult PromoImageLeft()
        {
            return View(Component);
        }

        public ViewResult PromoImageRight()
        {
            return View(Component);
        }

        public ViewResult LeftImageCTA()
        {
            return View(Component);
        }

        public ViewResult RightImageCTA()
        {
            return View(Component);
        }
    }
}