define(function (require) {
    // include components
    var http = require('durandal/http');
    var urlSources = require('dataservices/urlsources');
    
    // CategoryDataService class
    var ItemDataService = {
        listOfItems: function (params) {
            var serviceUrl = urlSources.listOfItemsUrl(params.page);
            return http.get(serviceUrl);
        }
    };

    return ItemDataService;
});