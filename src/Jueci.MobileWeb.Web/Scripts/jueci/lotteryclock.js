$(function () {
    nextLotteryClock();
});


function nextLotteryClock() {
    var _interval = 1000;
    
    var nextLotteryTime = Clock.countdown(Clock.getEndDate($('#NextPeriodTimePoint').val()));
      // console.log(nextLotteryTime);
    var timer = null;
    if (!nextLotteryTime.isRequestServer) {
        if (nextLotteryTime.hour.isNeedDisplay) {
            $('.hour').removeClass("clock-hide");
            $('#hourTenNumber').text(nextLotteryTime.hour.hourTenNumber);
            $('#hourUnitNumber').text(nextLotteryTime.hour.hourUnitNumber);
        } else {
            $('.hour').addClass("clock-hide");
        }

        $('#minuteTenNumber').text(nextLotteryTime.minute.minuteTenNumber);
        $('#minuteUnitNumber').text(nextLotteryTime.minute.minuteUnitNumber);

        $('#secondTenNumber').text(nextLotteryTime.second.secondTenNumber);
        $('#secondUnitNumber').text(nextLotteryTime.second.secondUnitNumber);

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
        "url": abp.appPath + "api/Ssc",
        //"data": { 'id': $('#PlanId').val() },
        "dataType": "json",
        "type": 'get',
        "success": function (data) {
            var lotteryData = data["data"];

            if (lotteryData['currentPeriod'] > currentPeriod) {

                $('#CurrentPeriod').val(lotteryData['currentPeriod']);
                $('#CurrentPeriodDisplay').text(lotteryData['currentPeriodDisplay']);

                $('.number-circle.number-circle-normal').each(function (i) {
                    $(this).text(lotteryData['lotteryResult'][i]);
                });

                $('#NextPeriodDisplay').text(lotteryData['nextPeriodDisplay']);
                $('#NextPeriodTimePoint').val(lotteryData['nextPeriodTimePointDisplay']);
                $('.countdown-number-content').removeClass('clock-hide');

                // userPlanInfoList
                $.ajax({
                    "url": abp.appPath + "app/ssc/userPlanInfoList",
                    "data": { 'id': $('#PlanId').val() },
                    "dataType": "html",
                    "type": 'get',
                    "beforeSend": function () {
                        $.fn.Loader(true);
                    },
                    "success": function (data1) {
                        $('#userPlanInfoList').html(data1);
                        setTimeout("nextLotteryClock();", _interval);
                      
                    },
                    "complete": function () {
                        $.fn.Loader(false);
                    }
                });


            } else {
                $('.countdown-number-content').addClass('clock-hide');
                $('#NextPeriodDisplay').text("第" + lotteryData["nextPeriod"] + "期正在开奖,请稍等...");
                setTimeout("getNewLottery();", _interval * 3);

            }

        }
    });
}