define(['durandal/system', 'durandal/app', 'durandal/plugins/router', 'services/logger'],
    function (system, app, router, logger) {
        var shell = {
            activate: activate,
            router: router,
            search: search,
            logOff: logOff
        };
        
        return shell;

        //#region Internal Methods
        function activate() {
            return boot();
        }
        
        function search() {
            app.showMessage('Search not yet implemented...');
        }
        
        function logOff() {
            $('<form action="/Account/LogOff"></form>').appendTo('body').submit();
        }

        function boot() {
            router.mapNav('dashboard');
            router.mapNav('categories');
            router.mapNav('items');
            router.mapRoute('editcategorymodel/:id', 'viewmodels/editcategorymodel', 'editcategorymodel', false);
            router.mapRoute('addcategorymodel/:id', 'viewmodels/addcategorymodel', 'addcategorymodel', false);
            // log('Administration Page Loaded!', null, true);
            return router.activate('dashboard');
        }

        function log(msg, data, showToast) {
            logger.log(msg, data, system.getModuleId(shell), showToast);
        }
        //#endregion
    });