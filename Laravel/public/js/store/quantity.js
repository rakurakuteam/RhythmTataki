console.log('quantityJs load');

// 수량 카운트 && 가격 표시
function quantity(e){
    let prices = price();
    let total_price = price2();
    let target_id = e.target.id;
    let id = target_id.split('_');
    let target_name = id[0];
    let target_num = id[id.length-1];
    let quantity = $('#quantity_'+target_num).val();
    let delivery = $('.price_2').text();
    console.log($('#price_2').val());
    delivery = delivery.replace(',','');
    delivery = Number(delivery.replace('원',''));
    if(target_name == 'minus'){
        if(quantity>1){
            $('#quantity_'+target_num).val(--quantity);
        }
    }else{
        $('#quantity_'+target_num).val(++quantity);
    }
    $('.price_1').text(numberWithCommas(prices*quantity)+"원");
    if(prices*quantity < 30000){
        delivery = 2500;
        $('.price_2').text("2,500원");
        $('.price_3').text(numberWithCommas(prices*quantity+delivery)+"원");
    }else{
        $('.price_2').text("0원");
        $('.price_3').text(numberWithCommas(prices*quantity)+"원");
    }
    $('.price_4').text("합계"+numberWithCommas(total_price)+"원");
}

// 개별 금액 계산
function price(){
    var price = $('.price_text').text();
    price = price.replace(',', '');
    price = price.replace('원', '')

    return Number(price);
}

// 장바구니 합계 계산
function price2(){
    var total_price = 0;
    for(var i=0; i<$('.price_box').length; i++){
        // console.log(i);
        // $('#quantity_').val();
        // $('#price_text_').text();
        let quantity = $('#quantity_'+i).val();
        let price = $('#price_text_'+i).text();
        // console.log($('#quantity_'+i).val())
        // console.log($('#price_text_'+i).text())
        total_price += quantity*price;
        // console.log(total_price);
    }
    console.log(total_price);
    return Number(total_price);
}

// 3자리 콤마
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}