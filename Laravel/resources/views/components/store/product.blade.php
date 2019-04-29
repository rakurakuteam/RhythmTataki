<contents>
    <center>
        <div class="content_box">
            <!--이미지-->
            <div class="shop_top">
                <div class="image">
                    <img src="{{$path.$image}}" id="shop_img" />
                </div>
                <!--텍스트-->
                <div class="text">
                    <h1>{{$product->name}}</h1>
                    <p id="price_text">{{number_format($product->price)}}</p>
                </div>
            </div>
            <div class="tag">
                <b><p id="tag_text">상세설명</p></b>
            </div>
            <div class="detail_img">
                <img src="{{asset('images/pic/shop_detail_img.png')}}" id="detail_img">
            </div>
        </div>
    </center>
</contents>