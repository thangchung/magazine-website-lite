define(function (require) {
    "use strict";
    // include components
    var router = require("durandal/plugins/router");
    var datacontext = require("datacontexts/categorydatacontext");
    var categoryModel = require("models/categorymodel");
    
    // EditCategoryViewModel class
    var EditCategoryViewModel = function () {
        this.id = ko.observable(0);
        this.name = ko.observable('');
    };

    EditCategoryViewModel.prototype.close = function () {
        this.modal.close();
    };

    EditCategoryViewModel.prototype.save = function () {
        var model = new categoryModel();
        model.name = this.name();
        model.id = this.id();

        datacontext.saveCategory(model);
        this.modal.close(model);
    };
    
    EditCategoryViewModel.prototype.activate = function (dataModel) {
        return true;
    };
    
    return EditCategoryViewModel;
});