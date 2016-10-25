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
        "success": function (data) {
            var lotteryData = data["data"];

            if (lotteryData['currentPeriod'] > currentPeriod) {

                $('.number-circle.number-circle-small').each(function (i) {
                    var j = i % lotteryData['lotteryResult'].length;
                    $(this).text(lotteryData['lotteryResult'][j]);
                });

                $('div[id*="tab"].tab-container').each(function (i) {
                    var tabIndex = i + 1;
                    $.ajax({
                        "url": abp.appPath + "app/CqSsc/userPlanDetailClock",
                        "data": { 'id': $('#PlanId').val(), 'planName': $('#planName' + tabIndex).val(), 'tabIndex': tabIndex },
                        "dataType": "html",
                        "type": 'get',
                        "success": function (data1) {
                            $('#tab-section1-' + tabIndex).html(data1);
                        }
                    });

                    $.ajax({
                        "url": abp.appPath + "app/CqSsc/userPlanDetailInfo",
                        "data": { 'id': $('#PlanId').val(), 'planName': $('#planName' + tabIndex).val() },
                        "dataType": "html",
                        "type": 'get',
                        "success": function (data2) {
                            $('#tab-section2-' + tabIndex).html(data2);
                        }
                    });

                    $.ajax({
                        "url": abp.appPath + "app/CqSsc/userPlanDetailList",
                        "data": { 'id': $('#PlanId').val(), 'planName': $('#planName' + tabIndex).val() },
                        "dataType": "html",
                        "type": 'get',
                        "success": function (data3) {
                            $('#tab-section3-' + tabIndex).html(data3);
                        }
                    });

                });
                setTimeout("nextLotteryClock();", _interval);

            } else {
                $('.clock').addClass('clock-hide');
                $('div[id*=nextPeriodDisplay]').removeClass('clock-hide');

                $('div[id*=nextPeriodDisplay]').text("开奖中,请稍等...");
                setTimeout("getNewLottery();", _interval * 3);

            }

        }
    });
}