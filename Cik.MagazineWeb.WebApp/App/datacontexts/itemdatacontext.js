define(function (require) {
    // include components
    var itemDataService = require('dataservices/itemdataservice');
    
    // ItemDataContext class
    var ItemDataContext = {
        listOfItems: function (params) {
            return itemDataService.listOfItems(params).then(function (data) {
                params.items(data.items);
                params.totalPage(data.totalPage);
                params.pagers(Utilities.buildPager(data.totalPage));
            });
        }
    };

    return ItemDataContext;
});