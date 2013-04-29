; (function ($, Cik, undefined) {
    UtilityClass = function () {
    };
    
    UtilityClass.prototype = {
        init: UtilityClass,
        rootUrl: '',
        pageSize: 10,
        buildPager: function(totalItem) {
            var totalPage = 0;
            if (totalItem <= Utilities.pageSize)
                totalPage = 1;
            else
                totalPage = totalItem / Utilities.pageSize + totalItem % Utilities.pageSize;
            var pageTemps = [];
            for (var i = 1; i <= totalPage; i++) {
                pageTemps.push({ page: i });
            }
            return pageTemps;
        }
    };
    
    Utilities = new UtilityClass();
})(jQuery);