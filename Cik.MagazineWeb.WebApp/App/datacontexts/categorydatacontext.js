define(function (require) {
    // include components
    var categoryDataService = require('dataservices/categorydataservice');
    
    // CategoryDataContext class
    var CategoryDataContext = {
        listOfCategories: function (params) {
            return categoryDataService.listOfCategories(params).then(function (data) {
                params.categories(data.categories);
                params.totalPage(data.totalPage);
                params.pagers(Utilities.buildPager(data.totalPage));
            });
        },
        getAllCategories: function () {
            return categoryDataService.getAllCategories().then(function (data) {
                return ko.observableArray(data);
            });
        },
        getCategoryById: function(id, categoryModel) {
            return categoryDataService.getCategoryById(id).then(function(data) {
                categoryModel.id(data.id);
                categoryModel.name(data.name);
            });
        },
        saveCategory: function(categoryData) {
            return categoryDataService.saveCategory(categoryData);
        },
        deleteCategory: function(id) {
            return categoryDataService.deleteCategory(id);
        }
    };

    return CategoryDataContext;
});