define(function (require) {
    // Item Model class
    var ItemModel = function () {
        this.id = ko.observable(0);
        this.categoryid = ko.observable(0);
        
        // Item Content region
        this.title = ko.observable('');
        this.shortdescription = ko.observable('');
        this.content = ko.observable('');
        this.smallimageurl = ko.observable('');
        this.mediumimageurl = ko.observable('');
        this.bigimageurl = ko.observable('');
        this.numberofview = ko.observable(false);
    };

    return ItemModel;
});