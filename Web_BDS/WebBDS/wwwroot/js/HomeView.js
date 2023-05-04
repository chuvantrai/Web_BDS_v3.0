// $(document).ready(function() {
//     loadDataHome();
// });
loadDataHome();
function loadDataHome(){
    $.ajax({
        url: 'https://localhost:2001/Home/HomeData',
        method: 'GET',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            loadTop3News(response.Top3News);
            loadProduct(response.Top3DatNen,'#DatNen');
            loadProduct(response.Top3CanHo,'#CanHo');
            loadProduct(response.Top3NhaPho,'#NhaPho');
            loadProduct(response.Top3BietThu,'#BietThu');
        },
        error: function(xhr, status, error) {
            console.log(222,error,xhr, status);
        }
    });
}

function loadTop3News(listNews){
    var news1 = `<img src="/myfiles/`+listNews[0].ImgAvar+`" alt="">
            <div class="trend-top-cap">
                <span style="background-color: #CCE5FF">Xem</span>
                <div style="display: flex">
                    <h2 class="title-news-item-home">
                        <a>`+listNews[0].Title+`</a>
                    </h2>
                    <p class="title-news-item-home title-news-item-home-2 "><i class="bi bi-calendar-minus"></i>`+FormatDate(listNews[0].DateUp)+`</p>
                </div>
            </div>`;
    var news2 ="";
    listNews.forEach(function(item, index) {
        if (index!==0){
            news2+=`<div class="trand-right-single d-flex">
                        <div class="trand-right-img">
                            <img src="myfiles/`+item.ImgAvar+`" alt="">
                        </div>
                        <div class="trand-right-cap">
                            <div style="display: flex">
                                <span class="color3">#Hastag</span>
                                <p class="title-news-item-home-2 date-news-item2"><i class="bi bi-calendar-minus"></i>`+FormatDate(item.DateUp)+`</p>
                            </div>
                            <h4>
                                <a href="`+item.NewsId+`">`+item.Title+`</a>
                            </h4>
                        </div>
                    </div>`;
        }
    });

    $("#news-1").html(news1);
    $("#news-2").html(news2);
}
function loadProduct(listProducts,id){
    var product ="";
    listProducts.forEach(function(item, index) {
            product+=`<div class="weekly-single active">
                            <div class="weekly-img">
                                <img src="/myfiles/`+item.ImgAvar+`" alt="">
                            </div>
                            <div class="weekly-caption">
                                <div style="display: flex">
                                    <span class="color1">Strike</span>
                                    <p class="price-item1 color-price">`+item.LetterPrice+` VND</p>
                                </div>
                                <h4>
                                    <a href="`+item.ProductId+`" class="title-pro-home">`+item.ProductName+`</a>
                                </h4>
                            </div>
                        </div>`;
    });

    $(id).html(product);
}
function FormatDate(dateOld){
    const date = new Date(dateOld);

    const day = date.getDate();
    const month = date.getMonth() + 1; // Tháng trong JavaScript tính từ 0 -> 11
    const year = date.getFullYear();
    const formattedDate = `${day}/${month}/${year}`;
    return formattedDate;
}