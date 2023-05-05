loadDataNewsDetail();
function loadDataNewsDetail(){
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const id = urlParams.get('id');
    $.ajax({
        url: '/News/NewsDetailData?id='+id,
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            // loadTop3News(response.top3News);
            // loadNewsDetail(response.newsDetail,'#NewsDetail');
            $("#MainIMG").html(`<img class="img-fluid" src="/myfiles/`+response.newsDetail.imgAvar+`" alt="">`);
            $("#Title").html(response.newsDetail.title);
            $("#Content").html(response.newsDetail.content);
            $("#Date").html(FormatDate(response.newsDetail.dateUp));

            var listNewsHTML ="";
            response.top3News.forEach(function(item, index) {
                    listNewsHTML+=`
                        <div class="media post_item" >
                        <img src="/myfiles/`+item.imgAvar+`" alt="post">
                        <div class="media-body">
                           <a href="`+item.newsId+`">
                              <h3 class="text-col2">`+item.title+`</h3>
                           </a>
                           <p>`+FormatDate(item.dateUp)+`</p>
                        </div>
                        </div>`;
            });
            $("#NewsMore").html(listNewsHTML);
        },
        error: function(xhr, status, error) {
            console.log(222,error,xhr, status);
        }
    });
}
function FormatDate(dateOld){
    const date = new Date(dateOld);

    const day = date.getDate();
    const month = date.getMonth() + 1; // Tháng trong JavaScript tính từ 0 -> 11
    const year = date.getFullYear();
    const formattedDate = `${day}/${month}/${year}`;
    return formattedDate;
}