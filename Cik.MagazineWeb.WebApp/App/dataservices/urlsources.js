define(function (require) {
    // UrlSources class
    var UrlSources = {
        // category urls
        listOfCategoriesUrl: function(page) {
            return Utilities.rootUrl + 'api/dashboardapi/categorypaging/?page=' + page;
        },
        getCategoryByIdUrl: function(id) {
            return Utilities.rootUrl + 'api/dashboardapi/getcategorybyid/?id=' + id;
        },
        saveCategoryUrl:Utilities.rootUrl + 'api/dashboardapi/savecategory',
        deleteCategoryUrl: function(id) {
            return Utilities.rootUrl + 'api/dashboardapi/deletecategory/?id=' + id;
        },
        //item urls
        listOfItemsUrl: function (page) {
            return Utilities.rootUrl + 'api/itemapi/itempaging/?page=' + page;
        },
    };

    return UrlSources;
});