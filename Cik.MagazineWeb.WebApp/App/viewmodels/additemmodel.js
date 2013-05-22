define(function (require) {
    "use strict";
    // include components
    var dataContext = require("datacontexts/itemdatacontext");
    var itemModel = require("models/itemmodel");

    // AddItemViewModel class
    var AddItemViewModel = function () {
        
        // Init view model
        this.title = ko.observable('');
        this.shortdescription = ko.observable('');
        this.content = ko.observable('');
        this.smallimageurl = ko.observable('');
        this.mediumimageurl = ko.observable('');
        this.bigimageurl = ko.observable('');
        this.numberofview = ko.observable(false);
    };

    AddItemViewModel.prototype.close = function () {
        this.modal.close();
    };

    AddItemViewModel.prototype.save = function () {
        var model = new itemModel();
        
        // Init view model
        model.title = this.title;
        model.shortdescription = this.shortdescription;
        model.content = this.content;
        model.smallimageurl = this.smallimageurl;
        model.mediumimageurl = this.mediumimageurl;
        model.bigimageurl = this.bigimageurl;
        model.numberofview = this.numberofview;

        if (dataContext == null || dataContext == undefined) return;
        this.modal.close(dataContext.saveItem(model));
    };

    AddItemViewModel.prototype.activate = function (dataModel) {
        return true;
    };

    return AddItemViewModel;
});