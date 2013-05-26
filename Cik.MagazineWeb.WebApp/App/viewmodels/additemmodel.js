define(function(require) {
    "use strict";
    // include components
    var itemDataContext = require("datacontexts/itemdatacontext");
    var categoryDataContext = require("datacontexts/categorydatacontext");
    var itemModel = require("models/itemmodel");

    // AddItemViewModel class
    var AddItemViewModel = function() {

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

    /*
    AddItemViewModel.prototype = {
        active : function() {
            return categoryDataContext.getAllCategories(this.categories);
        },

        close: function() {
            this.modal.close();
        },
        
        save : function () {
            var model = new itemModel();
        
            // Init view model
            model.title = this.title;
            model.shortdescription = this.shortdescription;
            model.content = this.content;
            model.smallimageurl = this.smallimageurl;
            model.mediumimageurl = this.mediumimageurl;
            model.bigimageurl = this.bigimageurl;
            model.numberofview = this.numberofview;
            model.categoryid = this.categoryId;

            if (itemDataContext == null || itemDataContext == undefined) return;
            this.modal.close(itemDataContext.saveItem(model));
        }
    };*/

    AddItemViewModel.prototype.activate = function () {
        return categoryDataContext.getAllCategories(this.categories);
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
        model.categoryid = this.categoryId;

        if (itemDataContext == null || itemDataContext == undefined) return;
        this.modal.close(itemDataContext.saveItem(model));
    };
    
    return AddItemViewModel;
});