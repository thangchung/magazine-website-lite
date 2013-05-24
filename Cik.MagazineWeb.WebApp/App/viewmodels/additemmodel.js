define(function (require) {
    "use strict";
    // include components
    var itemDataContext = require("datacontexts/itemdatacontext");
    var categoryDataContext = require("datacontexts/categorydatacontext");
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
        // this.categories = personArray; //ko.observableArray(personArray); //categoryDataContext.getAllCategories();
    };
    
    function person(id, name, age) {
        this.id = id;
        this.name = name;
        this.age = age;
    };

    var personArray =
        [new person(1, "1", 1),
        new person(2, "2", 2),
        new person(3, "3", 3)];
    
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

        if (itemDataContext == null || itemDataContext == undefined) return;
        this.modal.close(itemDataContext.saveItem(model));
    };

    AddItemViewModel.prototype.activate = function () {
        //return AddItemViewModel.prototype.getAllCategories();
        return true;
    };

    AddItemViewModel.prototype.getAllCategories = function () {
        categoryDataContext.getAllCategories(this.categories);
    };

    //function buildParametters() {
    //    return {
    //        categories: CategoriesViewModel.categoryList.categories,
    //        totalPage: CategoriesViewModel.categoryList.totalCategory,
    //        page: CategoriesViewModel.categoryList.currentPage,
    //        pagers: CategoriesViewModel.categoryList.pagers
    //    };
    //}

    return AddItemViewModel;
});