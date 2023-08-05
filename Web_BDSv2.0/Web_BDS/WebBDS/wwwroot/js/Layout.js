function InputMask(id,digits) {
    $("#"+id).inputmask({
        alias: "numeric",
        groupSeparator: ",",
        radixPoint: ".",
        autoGroup: true,
        digits: digits,
        digitsOptional: false,
        placeholder: "0",
        rightAlign: false
    });
}
function GetTimeDifference (date) {
    const targetDate = moment(date);
    const currentDate = moment();

    const minutesDifference = targetDate.diff(currentDate, "minutes");
    const hoursDifference = targetDate.diff(currentDate, "hours");
    const daysDifference = targetDate.diff(currentDate, "days");
    
    if(minutesDifference > -60){
        return (minutesDifference*-1)+' phút trước';
    }
    if(hoursDifference > -24){
        debugger
        return (hoursDifference*-1)+' giờ trước';
    }
    if(daysDifference > -7){
        return  (daysDifference*-1)+' ngày trước';
    }
    return  targetDate.format('L');
}

function OpenLoading() {
    $("#loading-overlay").fadeIn();
}
function OffLoading() {
    $("#loading-overlay").fadeOut();
}