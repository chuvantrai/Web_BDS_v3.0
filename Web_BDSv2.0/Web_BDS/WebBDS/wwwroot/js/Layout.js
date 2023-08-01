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