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

    const daysDifference = targetDate.diff(currentDate, "days");
    
    if(daysDifference===0){
        return 'hôm nay';
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

function LoadPageItem(pageIndex, totalPage, pageId) {
    let pageHtmlId = $(`#${pageId}`);
    let html = '';
    if (pageIndex-3>0){
        html += `<li class="page-item btn-page ml-1 mr-1 text-color2" onclick="ChangePage(1)">
                       <<
                   </li>`;
    }

    if (pageIndex-2>0){
        html += `<li class="page-item btn-page ml-1 mr-1" onclick="ChangePage(${pageIndex-2})">
                     <a class="page-link" >${pageIndex-2}</a>
                  </li>`;
    }

    if (pageIndex-1>0){
        html += `<li class="page-item btn-page ml-1 mr-1" onclick="ChangePage(${pageIndex-1})">
                    <a class="page-link" >${pageIndex-1}</a>
                  </li>`;
    }

    html += `<li class="page-item btn-page ml-1 mr-1 color3">
                <a class="page-link" >${pageIndex}</a>
             </li>`;

    if (pageIndex<totalPage){
        html += `<li class="page-item btn-page ml-1 mr-1" onclick="ChangePage(${pageIndex+1})">
                      <a class="page-link" >${pageIndex+1}</a>
                   </li>`;
    }

    if (pageIndex+1<totalPage){
        html += `<li class="page-item btn-page ml-1 mr-1" onclick="ChangePage(${pageIndex+2})">
                     <a class="page-link" >${pageIndex+2}</a>
                   </li>`;
    }

    if (pageIndex+2<totalPage){
        html += `<li class="page-item btn-page ml-1 mr-1 text-color2" onclick="ChangePage(${totalPage})">
                       >>
                 </li>`;
    }
    pageHtmlId.html(html);
}