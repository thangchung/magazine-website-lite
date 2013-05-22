define(function (require) {
    // include components
    var http = require('durandal/http');
    var urlSources = require('dataservices/urlsources');
    
    // CategoryDataService class
    var ItemDataService = {
        listOfItems: function (params) {
            var serviceUrl = urlSources.listOfItemsUrl(params.page);
            return http.get(serviceUrl);
        },
        
        getItemId: function(id) {
        var serviceUrl = urlSources.getItemByIdUrl(id);
        return http.get(serviceUrl);
        },
        
        saveItem: function(itemData) {
            var serviceUrl = urlSources.saveItemUrl;
            return http.post(serviceUrl, itemData);
        },
        
        deleteItem: function(id) {
            var serviceUrl = urlSources.deleteItemUrl(id);
            return http.get(serviceUrl);
        }
    };

    return ItemDataService;
});