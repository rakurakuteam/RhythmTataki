<contents>
    <center>
        <!-- 1.제품확인 -->
        <div class="order_box_1">
            <h3 class="title_text">제품확인</h3>
        </div>
        @foreach($products as $product)
        <div class="order_under_box_1">
            <img src="{{$product->images[0]->path.$product->images[0]->name}}" class="order_img">
            <div class="order_text_box">
                <h2 class="product_name">{{$product->name}}</h2>
                <p class="price_text" id="price_text_{{$count}}">{{number_format($product->price)}}원</p>
            </div>
            @include('components.store.quantityButton', ['count' => $count++])
        </div>
        @endforeach
        <!-- 2.금액 및 결제수단 -->
        <div class="order_box_2">
            <h3 class="title_text">금액 및 결제수단</h3>
        </div>
        <form action="{{route('store.order')}}" method="POST">
        @csrf
        <div class="order_under_box_2">
            <!-- 라디오박스 -->
            <div class="radio_box">
                <div id="radio">
                  <!-- 결제 방법 -->
                    <input type="radio" name="pos" id="pos1" value="credit_card" checked><br/>
                    <input type="radio" name="pos" id="pos2" value="cell_phone"><br/>
                    <input type="radio" name="pos" id="pos3" value="bank_book"><br/>
                    <input type="radio" name="pos" id="pos4" value="account_transfer"><br/>

                    <p class="pos">
                        <label for="pos1"></label>신용카드 결제&nbsp;&nbsp;
                        <label for="pos2"></label>핸드폰 결제&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <label for="pos3"></label>무통장 입금&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <label for="pos4"></label>실시간 계좌이체
                    </p>
                </div>
            </div>

            <div class="final_price_text">
                <h2>주문금액</h2>
                <h2 class="price_1">{{number_format($total_price)}}원</h2>
                <h2>배송비</h2>
                <h2 class="price_2" id="price_2">{{number_format($delivery)}}원</h2>
                <hr>
                <h2 class="all_price_text">총주문금액</h2>
                
                <input type="hidden" name="price" id="total_price" value="{{$total_price}}">
                <h2 class="price_3">{{number_format($total_price)}}원</h2>
            </div>

        </div>

        <!-- 3.배송지 설정 -->
        <div class="order_box_3">
            <h3 class="title_text">배송지 설정</h3>
        </div>
        <div class="order_under_box_3">
            <!--주문자이름박스-->
            <div class="buyer_box">
                <p class="buyer_name_text" id="user"><b>주문자</b></p>
                <div class="search_form">
                    <input type="text" value="{{$user->name}}" id="name">
                </div>
                <div class="search_form">
                    <input type="text" value="{{$user->phone}}" id="phone">
                </div>
            </div>
            <!--주소박스-->
            <div class="address_box">
                <p class="address_name_text"><b>배송지</b></p>
                <div class="address_under_box"></div>
                <!-- 체크 할 경우  -->
                <input type="checkbox" id="cb_1" name="" onclick="check_same_order()">
                <label for="cb_1"></label>
                <p class="cb_1_text">주문자와 같음</p>

                <div class="name_form">
                    <input type="text" placeholder="이름" id="delivery_name" name="delivery_name">
                </div>
                <div class="number_form">
                    <input type="text" placeholder="연락처" id="delivery_address" name="delivery_address">
                </div>
                <!-- 체크 할 경우  -->
                <input type="checkbox" id="cb_2" name="cb_2">
                <label for="cb_2"></label>
                <p class="cb_2_text">기본 배송지로</p>
                <button type="button" class="addr_find_btn" onclick="sample4_execDaumPostcode()">
                    <img src="{{asset("images/pic/finder.png")}}" class="finder_icon"/>
                    <b>주소찾기</b>
                </button>
                <div class="code_form">
                    <input type="text" placeholder="우편번호" id="zip_code" name="zip_code">
                </div>
                <div class="addr_form_1">
                    <input type="text" placeholder="주소" id="address" name="address1">
                </div>
                <div class="addr_form_2">
                    <input type="text" placeholder="상세주소" name="address2">
                </div>

                <p class="message_text"><b>배송 메세지</b></p>
                <div class="message_form">
                    <input type="text" placeholder="배송메세지" name="order_request">
                </div>
            </div>

            <div class="final_btn_box">
                <button type="submit" class="final_btn">
                    <b>주문완료</b>
                </button>
            </div>
        </div>
        </form>
    </center>
</contents>
