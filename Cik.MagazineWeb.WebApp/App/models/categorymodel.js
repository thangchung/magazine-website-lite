define(function (require) {
    // AddCategoryViewModel class
    var CategoryModel = function() {
        this.name = ko.observable('');
        this.id = ko.observable(0);
    };

    return CategoryModel;
});