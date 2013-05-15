define(function (require) {
    // include components
    var app = require('durandal/app');
    var logger = require('services/logger');
    var modelDialog = require('durandal/modaldialog');
    var categoryDataContext = require('datacontexts/categorydatacontext');
    var addCategoryModel = require('viewmodels/addcategorymodel');
    var editCategoryModel = require('viewmodels/editcategorymodel');

    // CategoriesViewModel class
    var CategoriesViewModel = {
        activate: activate,
        categoryList: {
            categories: ko.observableArray([]),
            pagers: ko.observableArray([]),
            currentPage: ko.observable(0),
            totalCategory: ko.observable(0),
            paging: paging
        },
        addCategory: addCategory,
        editCategory: editCategory,
        deleteCategory: deleteCategory,
        closeAddCategoryWindow: closeAddCategoryWindow
    };

    return CategoriesViewModel;

    //#region Internal Methods
    function activate() {
        CategoriesViewModel.categoryList.currentPage = 1;
        return reloadListOfCategories();
    }

    function paging(page) {
        CategoriesViewModel.categoryList.currentPage = page; // update current page
        return reloadListOfCategories();
    }

    function addCategory(item) {
        return modelDialog
            .show(new addCategoryModel())
            .then(function(response) {
                reloadListOfCategories();
            });
    }

    function editCategory(dataRow) {
        var model = new editCategoryModel();
        return categoryDataContext
            .getCategoryById(dataRow.id, model)
            .then(function(data) {
                modelDialog.show(model).then(function(response) {
                    reloadListOfCategories();
                });
            });
    }

    function deleteCategory(id) {
        return app
            .showMessage('Do you want to delete?', 'Delete category', ['Yes', 'No'])
            .then(function(answer) {
                if (answer === 'Yes') {
                    categoryDataContext.deleteCategory(id).then(function() {
                        reloadListOfCategories();
                    });
                }
            });
    }

    function closeAddCategoryWindow(dialog) {

    }
    
    function reloadListOfCategories() {
        categoryDataContext.listOfCategories(buildParametters());
    }

    function buildParametters() {
        return {
            categories: CategoriesViewModel.categoryList.categories,
            totalPage: CategoriesViewModel.categoryList.totalCategory,
            page: CategoriesViewModel.categoryList.currentPage,
            pagers: CategoriesViewModel.categoryList.pagers
        };
    }
    //#endregion
});