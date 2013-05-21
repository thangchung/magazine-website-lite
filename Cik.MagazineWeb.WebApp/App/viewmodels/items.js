define(function (require) {
    // include components
    var app = require('durandal/app');
    var logger = require('services/logger');
    var modelDialog = require('durandal/modaldialog');
    var itemDataContext = require('datacontexts/itemdatacontext');
    var addItemModel = require('viewmodels/additemmodel');

    // ItemsViewModel class
    var ItemsViewModel = {
        activate: activate,
        itemList: {
            items: ko.observableArray([]),
            pagers: ko.observableArray([]),
            currentPage: ko.observable(0),
            totalRecord: ko.observable(0),
            paging: paging,
            addItem: addItem
        }
    };

    return ItemsViewModel;

    //#region Internal Methods

    // Implement Add Item feture
    // Added by Toan Le on 21/05/2013
    function addItem(item) {
        return modelDialog
            .show(new addItemModel())
            .then(function (response) {
                reloadListOfItems();
            });
    }

    function activate() {
        ItemsViewModel.itemList.currentPage = 1;
        return reloadListOfItems();
    }

    function paging(page) {
        ItemsViewModel.itemList.currentPage = page; // update current page
        return reloadListOfItems();
    }
    
    function reloadListOfItems() {
        return itemDataContext.listOfItems(buildParametters());
    }

    function buildParametters() {
        return {
            items: ItemsViewModel.itemList.items,
            totalPage: ItemsViewModel.itemList.totalRecord,
            page: ItemsViewModel.itemList.currentPage,
            pagers: ItemsViewModel.itemList.pagers
        };
    }
    //#endregion
});