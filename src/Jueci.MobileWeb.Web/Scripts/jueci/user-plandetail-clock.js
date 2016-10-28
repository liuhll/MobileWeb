$(function () {
    nextLotteryClock();
});

function nextLotteryClock() {
    var _interval = 1000;
    var nextLotteryTime = Clock.countdown(Clock.getEndDate($('#NextPeriodTimePoint').val()));
    //   console.log(nextLotteryTime);
    var timer = null;
    // console.log(nextLotteryTime);
    if (!nextLotteryTime.isRequestServer) {
        if (nextLotteryTime.hour.isNeedDisplay) {
            $('.hour').removeClass("clock-hide");
            $("div[id*='hourTenNumber']").text(nextLotteryTime.hour.hourTenNumber);
            $("div[id*='hourUnitNumber']").text(nextLotteryTime.hour.hourUnitNumber);
        } else {
            $('.hour').addClass("clock-hide");
        }

        $("div[id*='minuteTenNumber']").text(nextLotteryTime.minute.minuteTenNumber);
        $("div[id*='minuteUnitNumber']").text(nextLotteryTime.minute.minuteUnitNumber);

        $("div[id*='secondTenNumber']").text(nextLotteryTime.second.secondTenNumber);
        $("div[id*='secondUnitNumber']").text(nextLotteryTime.second.secondUnitNumber);

        timer = setTimeout("nextLotteryClock();", _interval);
        // getNewLottery();
    } else {
        getNewLottery();
    }
}

function getNewLottery() {
    var currentPeriod = parseInt($('#CurrentPeriod').val());
    var _interval = 1000;
    $.ajax({
        "url": abp.appPath + "api/Lottery",
        "dataType": "json",
        "type": 'get',
        "data": { 'cpType': $('#cptype').val() },
        "success": function (data) {
            var lotteryData = data["data"];

            if (lotteryData['currentPeriod'] > currentPeriod) {

                $.ajax({
                    "url": abp.appPath + "app/" + $("#cptype").val() + "/userPlanDetailContent",
                    "data": { 'id': $('#PlanId').val(), "currentTabIndex": $('#CurrentTabIndex').val() },
                    "dataType": "html",
                    "type": 'get',
                    "beforeSend": function () {
                        Loader(true);
                          
                    },
                    "success": function (data3) {
                        if (data3.indexOf('>') > 0) {
                            $('#userPlanDetailContent').html(data3);
                            setTimeout("nextLotteryClock();", _interval);
                        } else {
                            var resultObj = JSON.parse(data3);
                            var arData = resultObj["result"];
                            if (arData["returnUrl"].indexOf('?') > 0) {
                                location.href = abp.appPath + arData["returnUrl"];
                            } else {
                                location.href = abp.appPath + arData["returnUrl"] + "?jueci";
                            }
                        }

                    },
                    "complete": function () {
                        Loader(false);
                          
                    }

                });


            } else {
                $('.clock').addClass('clock-hide');
                $('div[id*=nextPeriodDisplay]').removeClass('clock-hide');

                $('div[id*=nextPeriodDisplay]').text("开奖中,请稍等...");
                setTimeout("getNewLottery();", _interval * 3);

            }

        }
    });
}

function Loader(show) {
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

