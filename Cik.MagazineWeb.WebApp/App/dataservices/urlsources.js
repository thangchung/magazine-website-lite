define(function (require) {
    // UrlSources class
    var UrlSources = {
        
        // category urls
        listOfCategoriesUrl: function (page) {
            return Utilities.rootUrl + 'api/categoryapi/categorypaging/?page=' + page;
        },
        getCategoryByIdUrl: function (id) {
            return Utilities.rootUrl + 'api/categoryapi/getcategorybyid/?id=' + id;
        },
        saveCategoryUrl: Utilities.rootUrl + 'api/categoryapi/savecategory',
        deleteCategoryUrl: function (id) {
            return Utilities.rootUrl + 'api/categoryapi/deletecategory/?id=' + id;
        },
        
        ///******** Refactoring Code: Change Url to Category Api by Toan Le on 21/05/2013 ********/
        ///* 
        //listOfCategoriesUrl: function(page) {
        //    return Utilities.rootUrl + 'api/dashboardapi/categorypaging/?page=' + page;
        //},
        //getCategoryByIdUrl: function(id) {
        //    return Utilities.rootUrl + 'api/dashboardapi/getcategorybyid/?id=' + id;
        //},
        //saveCategoryUrl:Utilities.rootUrl + 'api/dashboardapi/savecategory',
        //deleteCategoryUrl: function(id) {
        //    return Utilities.rootUrl + 'api/dashboardapi/deletecategory/?id=' + id;
        //},
        //*/

        //item urls
        listOfItemsUrl: function (page) {
            return Utilities.rootUrl + 'api/itemapi/itempaging/?page=' + page;
        },
    };

    return UrlSources;
});