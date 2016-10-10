
(function ($) {
    $.fn.Loader = function (show) {
        if (show) {
            //显示加载器.for jQuery Mobile 1.2.0  
            $.mobile.loading('show', {
                text: '加载中...', //加载器中显示的文字  
                textVisible: true, //是否显示文字  
                theme: 'a',        //加载器主题样式a-e  
                textonly: false,   //是否只显示文字  
                html: ""           //要显示的html内容，如图片等  
            });
        } else {
            $.mobile.loading('hide');
        }
        
    }

})(jQuery);