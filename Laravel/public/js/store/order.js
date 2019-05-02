function sample4_execDaumPostcode() {
    let width = 500;
    let height = 600;
    new daum.Postcode(
    {
        // popupName: 'postcodePopup',
        width: width,
        height: height,
        oncomplete : function(data) {
            // 팝업에서 검색결과 항목을 클릭했을때 실행할 코드를 작성하는 부분.

            // 도로명 주소의 노출 규칙에 따라 주소를 조합한다.
            // 내려오는 변수가 값이 없는 경우엔 공백('')값을 가지므로, 이를 참고하여 분기 한다.
            var fullRoadAddr = data.roadAddress; // 도로명 주소 변수
            console.log(fullRoadAddr);
            var extraRoadAddr = ''; // 도로명 조합형 주소 변수

            // 법정동명이 있을 경우 추가한다. (법정리는 제외)
            // 법정동의 경우 마지막 문자가 "동/로/가"로 끝난다.
            if (data.bname !== ''
                    && /[동|로|가]$/g.test(data.bname)) {
                extraRoadAddr += data.bname;
            }
            // 건물명이 있고, 공동주택일 경우 추가한다.
            if (data.buildingName !== ''
                    && data.apartment === 'Y') {
                extraRoadAddr += (extraRoadAddr !== '' ? ', '
                        + data.buildingName : data.buildingName);
            }
            // 도로명, 지번 조합형 주소가 있을 경우, 괄호까지 추가한 최종 문자열을 만든다.
            if (extraRoadAddr !== '') {
                extraRoadAddr = ' (' + extraRoadAddr + ')';
            }
            // 도로명, 지번 주소의 유무에 따라 해당 조합형 주소를 추가한다.
            if (fullRoadAddr !== '') {
                fullRoadAddr += extraRoadAddr;
            }
            // 우편번호와 주소 정보를 해당 필드에 넣는다.
            document.getElementById('zip_code').value = data.zonecode; //5자리 새우편번호 사용
            if(data.addressType == "R"){
                document.getElementById('address').value = fullRoadAddr; // 도로명 주소
            }else{
                document.getElementById('address').value = data.jibunAddress; // 지번 주소
            }
        }
    }).open({
        left: (window.screen.width / 2) - (width / 2),
        top: (window.screen.height / 2) - (height / 2)
    });
}

function delivery_user(){
    let id = $('#id').id();
    let name = $('#name').id();
    if(document.getElementById('cb_1').checked == true){
        $('#delivery_name').text(id);
        $('#delivery_name').text(name);
    }
}
function check_same_order(){
    //체크박스
    let checkbox = document.getElementById('cb_1');
    //주문자 박스
    let buyer_box_name = document.getElementById('name');
    let buyer_box_phone = document.getElementById('phone');
    //주소박스
    let address_box_name = document.getElementById('delivery_name');
    let address_box_phone = document.getElementById('delivery_address');
  
    // onclick 이벤트가 일어난 후에 check가 됨으로 checked의 조건을 반대로함
    if(checkbox.checked !== false){
      address_box_name.value = buyer_box_name.value;
      address_box_phone.value = buyer_box_phone.value;
    }else{
      address_box_name.value = "";
      address_box_phone.value = "";
    }
  }