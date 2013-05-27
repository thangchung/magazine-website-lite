define(function (require) {
    "use strict";
    // include components
    var itemDataContext = require("datacontexts/itemdatacontext");
    var categoryDataContext = require("datacontexts/categorydatacontext");
    var itemModel = require("models/itemmodel");

    // AddItemViewModel class
    var EditItemViewModel = function () {
        this.id = ko.observable(0);
        this.categoryid = ko.observable(0);

        // Init view model
        this.title = ko.observable('');
        this.shortdescription = ko.observable('');
        this.content = ko.observable('');
        this.smallimageurl = ko.observable('');
        this.mediumimageurl = ko.observable('');
        this.bigimageurl = ko.observable('');
        this.categories = ko.observableArray([]);
        this.categoryId = ko.observable(0);
    };

    EditItemViewModel.prototype.activate = function () {
        return categoryDataContext.getAllCategories(this.categories);
    };

    EditItemViewModel.prototype.close = function () {
        this.modal.close();
    };

    EditItemViewModel.prototype.save = function () {
        var model = new itemModel();

        // Init view model
        model.id = this.id;
        model.title = this.title;
        model.shortdescription = this.shortdescription;
        model.content = this.content;
        model.smallimageurl = this.smallimageurl;
        model.mediumimageurl = this.mediumimageurl;
        model.bigimageurl = this.bigimageurl;
        model.categoryid = this.categoryId;

        if (itemDataContext == null || itemDataContext == undefined) return;
        this.modal.close(itemDataContext.saveItem(model));
    };

    return EditItemViewModel;
});