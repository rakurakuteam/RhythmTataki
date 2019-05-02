<?php

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/
// 메인 페이지
Route::get('/', 'HomeController@index')->name('home');

// 유니티 관련
Route::prefix('unity')->group(function() {
    Route::get('/login', 'UnityController@loginForm')->name('unity.loginPage'); // 로그인 페이지
    Route::get('/setScore/{email}/{song}/{score}', 'UnityController@setScore')->name('unity.setScore'); // 스코어 등록
    Route::get('/getScore/{email}/{song}', 'UnityController@getScore')->name('unity.getScore'); // 점수 조회 
    Route::get('/getScores/{email}', 'UnityController@getScores')->name('unity.getScores'); // 점수들 조회
    Route::get('/fileDownload/{email}', 'UnityController@fileDownload')->name('unity.fileDownload'); // 파일 다운로드
    Route::get('/getMusicList/{email}', 'UnityController@getMusicList')->name('unity.getMusicList'); // 음악 목록

    Route::post('/join', 'UnityController@join')->name('unity.join'); // 회원가입
    Route::post('/login', 'UnityController@login')->name('unity.login'); // 로그인
    Route::post('/fileUpload', 'UnityController@fileUpload')->name('unity.fileUpload'); // 파일 업로드
    Route::post('/test', 'UnityController@test');
});

// 로그인 관련
Auth::routes();

// 구글 로그인
Route::prefix('login')->group(function() {
    Route::get('/google', 'GoogleLoginController@googleLogin')->name('google.login'); // 구글 로그인
    Route::get('/google/callback', 'GoogleLoginController@googleCallback')->name('google.callback'); // 구글 로그인 콜백
});

// 게시글 관련
Route::post('/download', 'HomeController@download')->name('board.download'); // 다운로드 체크
Route::resource('board', 'HomeController');

Route::get('/myPage', 'HomeController@myPage')->name('myPage'); // 마이페이지
Route::get('/userPage/{id}', 'HomeController@userPage')->name('userPage'); // 유저페이지
Route::get('/pagination', 'HomeController@pagination')->name('pagination'); // 메인 페이지 페이지네이션

Route::post('/heartToggle', 'HomeController@heartToggle')->name('heart.toggle'); // 좋아요 처리

// 소리공방
Route::prefix('workshop')->group(function() {
    Route::get('/', 'WorkshopController@index')->name('workshop.main'); // 소리공방 메인
    Route::post('/upload', 'WorkshopController@upload')->name('workshop.upload'); // 소리 업로드
    Route::post('/cutter', 'WorkshopController@cutter')->name('workshop.cutter'); // 소리 자르기
});

// 주문관련
Route::prefix('store')->group(function() {
    Route::get('orderList', 'ProductsController@orderList')->name('orderListPage'); // 주문확인 페이지
    Route::get('orderSheet/{id}', 'ProductsController@orderSheet')->name('orderSheetPage'); // 주문 페이지
    Route::get('payPage', 'ProductsController@payPage')->name('payPage'); // 결제 페이지
    Route::get('cart', 'ProductsController@cartPage')->name('cartPage'); // 장바구니

    Route::post('removeCart', 'ProductsController@removeCart')->name('cart.remove'); // 장바구니 삭제
    Route::post('addCart', 'ProductsController@addCart')->name('cart.add'); // 장바구니 등록
    Route::post('order', 'ProductsController@order')->name('store.order'); // 주문처리
    Route::post('pay', 'ProductsController@pay')->name('store.pay'); // 결제처리
});

// 스토어 관련
Route::resource('store', 'StoreController');

// 유니티 테스트용
Route::get('fileUpload', function(){
    return view('components.board.uploadForm');
});
