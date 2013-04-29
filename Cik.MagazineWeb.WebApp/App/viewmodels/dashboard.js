define(function(require) {
    // include components
    var logger = require('services/logger');
    var categoryDataContext = require('datacontexts/categorydatacontext');
    var itemDataContext = require('datacontexts/itemdatacontext');

    // DashboardViewModel class
    var DashboardViewModel = {
        activate: activate,
        title: 'Dashboard View',
        categorySummary: {
            categories: ko.observableArray([]),
            pagers: ko.observableArray([]),
            currentPage: ko.observable(0),
            totalCategory: ko.observable(0),
            paging: categoryPaging
        },
        itemSummary: {
            items: ko.observableArray([]),
            pagers: ko.observableArray([]),
            currentPage: ko.observable(0),
            totalItem: ko.observable(0),
            paging: itemPaging
        }
    };

    return DashboardViewModel;

    //#region Internal Methods
    function activate() {
        DashboardViewModel.categorySummary.currentPage = 1;
        DashboardViewModel.itemSummary.currentPage = 1;
        
        categoryDataContext.listOfCategories(buildCategoryParametters());
        itemDataContext.listOfItems(buildItemParametters());
        
        return true;
    }

    function categoryPaging(page) {
        DashboardViewModel.categorySummary.currentPage = page; // update current page
        return categoryDataContext.listOfCategories(buildCategoryParametters());
    }
    
    function itemPaging(page) {
        DashboardViewModel.itemSummary.currentPage = page; // update current page
        return itemDataContext.listOfItems(buildItemParametters());
    }

    function buildCategoryParametters() {
        return {
            categories: DashboardViewModel.categorySummary.categories,
            totalPage: DashboardViewModel.categorySummary.totalCategory,
            page: DashboardViewModel.categorySummary.currentPage,
            pagers: DashboardViewModel.categorySummary.pagers
        };
    }
    function buildItemParametters() {
        return {
            items: DashboardViewModel.itemSummary.items,
            totalPage: DashboardViewModel.itemSummary.totalItem,
            page: DashboardViewModel.itemSummary.currentPage,
            pagers: DashboardViewModel.itemSummary.pagers
        };
    }
    //#endregion
});