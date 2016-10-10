var Clock = (function () {

    var clock = {};

    clock.countdown = function (endDate) {
        var now = new Date();
        var leftTime = endDate.getTime() - now.getTime();
        var leftsecond = parseInt(leftTime / 1000);
        var day1 = Math.floor(leftsecond / (60 * 60 * 24));
        var hour = Math.floor((leftsecond - day1 * 24 * 60 * 60) / 3600);
        var minute = Math.floor((leftsecond - day1 * 24 * 60 * 60 - hour * 3600) / 60);
        var second = Math.floor(leftsecond - day1 * 24 * 60 * 60 - hour * 3600 - minute * 60);
        var clockResult = {
            hour: {
                hourTenNumber: getTenNumber(hour),
                hourUnitNumber: getUnitNumber(hour),
                isNeedDisplay: isNeedDisplayHour(day1, hour)

            },
            minute: {
                minuteTenNumber: getTenNumber(minute),
                minuteUnitNumber: getUnitNumber(minute)
            },
            second: {
                secondTenNumber: getTenNumber(second),
                secondUnitNumber: getUnitNumber(second)
            },
            isRequestServer: now > endDate
    };

        return clockResult;

    }

    return clock;
})();

function getUnitNumber(number) {
    if (typeof number !== "number") {
        throw new error(number + "不是数字，无法获取该数字的个位数");
    }
    return number % 10;
}

function isNeedDisplayHour(day,hour) {
    if (day < 0 || hour <= 0 ) {
        return false;
    } else {
        return true;
    }
}

function getTenNumber(number) {
    if (typeof number !== "number") {
        throw new error(number + "不是数字，无法获取该数字的十位数");
    }
    return parseInt((number % 100) / 10);;
}