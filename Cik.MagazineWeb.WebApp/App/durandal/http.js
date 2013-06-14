define(function() {
    return {
        defaultJSONPCallbackParam:'callback',
        get:function(url, query) {
            // return $.ajax(url, { data: query }); 
            //style='display: none'

            var style = "style='display: block; margin: auto; backgroundColor: transparent; border: none; zIndex: 10002'";
            var imageLoading = "<img id='ImageProcessing' src='/Content/images/InProgress.gif' alt='Loading...' " + style + " />";
            
            // var styleDivLoading = "style = 'height: 400px; position: relative; background-color: gray; /* for demonstration */";
            // var divLoading = "<div id='LoadingImage' >" + imageLoading + "</div>";
            $('body').append(imageLoading);
            //$.blockUI();
            
            // show loading
            return $.ajax({
                url: url,
                data: query,
                type: 'GET',
                contentType: 'application/json',
                dataType: 'json',
                success: function() { 
                    //$.unblockUI();
                    $("#ImageProcessing").remove();
                }, 
                error: function (xhr, status) {
                    //$.unblockUI();
                    $("#ImageProcessing").remove();
                } 
            });
            
        },
        jsonp: function (url, query, callbackParam) {
            if (url.indexOf('=?') == -1) {
                callbackParam = callbackParam || this.defaultJSONPCallbackParam;

                if (url.indexOf('?') == -1) {
                    url += '?';
                } else {
                    url += '&';
                }

                url += callbackParam + '=?';
            }

            return $.ajax({
                url: url,
                dataType:'jsonp',
                data:query
            });
        },
        post:function(url, data) {
            return $.ajax({
                url: url,
                data: ko.toJSON(data),
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json'
            });
        }
    };
});