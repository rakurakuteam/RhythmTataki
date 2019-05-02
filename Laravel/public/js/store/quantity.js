console.log('quantityJs load');

// 수량 카운트 && 가격 표시
function quantity(e){
    let prices = price();
    let target_id = e.target.id;
    let id = target_id.split('_');
    let target_name = id[0];
    let target_num = Number(id[id.length-1]);
    let quantity = [];
    
    quantity[target_num] = $('#quantity_'+target_num).val();
    
    console.log("id : "+id);
    console.log('target_num : '+target_num);
    console.log('prices  :'+prices);
    console.log('quantity[target_num] : '+quantity[target_num]);
    console.log('quantity : '+quantity);
    let delivery = $('.price_2').text();
    
    delivery = delivery.replace(',','');
    delivery = Number(delivery.replace('원',''));
    // console.log($('#price_2').val());
    
    if(target_name == 'minus'){ // 마이너스 버튼이면
        if(quantity[target_num]>1){// 수량이 1보다 크면 -
            $('#quantity_'+target_num).val(--quantity[target_num]);
        }
    }else{ // -버튼이 아니면 수량 +
        $('#quantity_'+target_num).val(++quantity[target_num]);
    }
    let total_price = price2();

    // 주문 페이지 주문금액
    $('.price_1').text(numberWithCommas(total_price)+"원");

    if(total_price < 30000){ // 주문금액이 3만원 미만이면
        delivery = 2500;
        $('.price_2').text("2,500원"); // 배송비
        // 배달비 포함 총 주문 금액
        $('.price_3').text(numberWithCommas(total_price+delivery)+"원");
        $('#total_price').val(total_price+delivery);
    }else{ // 주문금액이 3만원 미안이 아니면
        $('.price_2').text("0원"); // 배송비
        // 총 주문 금액
        $('.price_3').text(numberWithCommas(total_price)+"원");
        $('#total_price').val(total_price);
    }
    // 장바구니 총 금액
    $('.price').text(numberWithCommas(total_price)+"원");
}

// 개별 금액 계산
function price(){
    var price = $('.price_text').text();
    price = price.replace(',', '');
    price = price.replace('원', '')

    return Number(price);
}

// 합계 계산
function price2(){
    var total_price = 0;
    for(var i=0; i<$('.quantity').length; i++){
        // console.log(i);
        // $('#quantity_').val();
        // $('#price_text_').text();
        let quantity = $('#quantity_'+i).val();
        let price = $('#price_text_'+i).text();
        price = price.replace(',','');
        price = Number(price.replace('원',''));
        // console.log($('#quantity_'+i).val())
        // console.log($('#price_text_'+i).text())
        total_price += quantity*price;
        // console.log(total_price);
    }
    console.log("total_price : "+total_price);
    return Number(total_price);
}

// 3자리 콤마
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}