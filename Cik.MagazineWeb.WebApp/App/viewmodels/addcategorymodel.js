define(function (require) {
    "use strict";
    // include components
    var datacontext = require("datacontexts/categorydatacontext");
    var categoryModel = require("models/categorymodel");

    // AddCategoryViewModel class
    var AddCategoryViewModel = function() {
        this.name = ko.observable('');
    };

    AddCategoryViewModel.prototype.close = function () {
        this.modal.close();
    };

    AddCategoryViewModel.prototype.save = function() {
        var model = new categoryModel();
        model.name = this.name();

        if (datacontext == null || datacontext == undefined) return;
        this.modal.close(datacontext.saveCategory(model)); 
    };
    
    AddCategoryViewModel.prototype.activate = function (dataModel) {
        return true;
    };

    return AddCategoryViewModel;
});