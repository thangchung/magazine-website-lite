define(function (require) {
    // include components
    var http = require('durandal/http');
    var urlSources = require('dataservices/urlsources');
    
    // CategoryDataService class
    var CategoryDataService = {
        listOfCategories: function (params) {
            var serviceUrl = urlSources.listOfCategoriesUrl(params.page);
            return http.get(serviceUrl);
        },
        getCategoryById: function(id, categoryModel) {
            var serviceUrl = urlSources.getCategoryByIdUrl(id);
            return http.get(serviceUrl);
        },
        saveCategory: function(categoryData) {
            var serviceUrl = urlSources.saveCategoryUrl;
            return http.post(serviceUrl, categoryData);
        },
        deleteCategory: function(id) {
            var serviceUrl = urlSources.deleteCategory(id);
            return http.get(serviceUrl);
        }
    };

    return CategoryDataService;
});