define(function() {
    return {
        defaultJSONPCallbackParam:'callback',
        get:function(url, query) {
            // return $.ajax(url, { data: query }); 
            $.blockUI({
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            });
            
            return $.ajax({
                url: url,
                data: query,
                type: 'GET',
                contentType: 'application/json',
                dataType: 'json',
                success: function() { 
                    $.unblockUI();
                }, 
                error: function () {
                    $.unblockUI();
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