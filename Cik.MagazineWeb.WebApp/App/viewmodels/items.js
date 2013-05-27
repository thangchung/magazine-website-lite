define(function (require) {
    // include components
    var app = require('durandal/app');
    var logger = require('services/logger');
    var modelDialog = require('durandal/modaldialog');
    var itemDataContext = require('datacontexts/itemdatacontext');
    var addItemModel = require('viewmodels/additemmodel');
    var editItemModel = require('viewmodels/edititemmodel');

    // ItemsViewModel class
    var ItemsViewModel = {
        activate: activate,
        itemList: {
            items: ko.observableArray([]),
            pagers: ko.observableArray([]),
            currentPage: ko.observable(0),
            totalRecord: ko.observable(0),
            paging: paging
        },
        addItem: addItem,
        editItem: editItem,
        deleteItem: deleteItem
    };

    return ItemsViewModel;

    //#region Internal Methods
    
    function editItem(dataRow) {
        var model = new editItemModel();
        return itemDataContext
            .getItemById(dataRow.itemId, model)
            .then(function () {
                modelDialog.show(model).then(function () {
                    reloadListOfItems();
                });
            });
    }
    
    function addItem() {
        return modelDialog
            .show(new addItemModel())
            .then(function () {
                reloadListOfItems();
            });
    }
    
    function deleteItem(id) {
        return app
            .showMessage('Do you want to delete?', 'Delete Item', ['Yes', 'No'])
            .then(function (answer) {
                if (answer === 'Yes') {
                    itemDataContext.deleteItem(id).then(function () {
                        reloadListOfItems();
                    });
                }
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