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
        },
        
        getItemById: function (id, itemModel) {
            return itemDataService.getItemById(id).then(function (data) {
                // data: is Item Model from server side
                // Binding Data manually
                itemModel.id(data.id);
                itemModel.categoryId(data.categoryId);

                // Item Content region
                itemModel.title(data.title);
                itemModel.shortdescription(data.shortDescription);
                itemModel.content(data.content);
                itemModel.smallimageurl(data.smallimageurl);
                itemModel.mediumimageurl(data.mediumimageurl);
                itemModel.bigimageurl(data.bigimageurl);
            });
        },
        
        saveItem: function (itemData) {
            return itemDataService.saveItem(itemData);
        },
        
        deleteItem: function (id) {
            return itemDataService.deleteItem(id);
        }
    };

    return ItemDataContext;
});