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
Route::get('/', 'HomeController@index');

// 구글 로그인
Route::prefix('login')->group(function () {
    Route::get('/google', 'GoogleLoginController@googleLogin')->name('google.login');
    Route::get('/google/callback', 'GoogleLoginController@googleCallback')->name('google.callback');
});

// 유니티 관련
Route::prefix('unity')->group(function () {
    Route::post('/join', 'UnityController@join')->name('unity.join');
    Route::post('/login', 'UnityController@login')->name('unity.login');
});

Auth::routes();

Route::prefix('home')->group(function () {
    Route::get('/', 'HomeController@index')->name('home'); // 메인페이지
    Route::get('/board/{id}', 'HomeController@show')->name('board'); // 게시판
    Route::get('/myPage', 'HomeController@myPage')->name('myPage'); // 마이페이지
});

Route::get('/userPage/{id}', 'HomeController@userPage')->name('userPage'); // 유저페이지

Route::post('/heartToggle', 'HomeController@heartToggle')->name('heart.toggle');
Route::get('/pagination', 'HomeController@pagination')->name('pagination'); // 메인 페이지 페이지네이션

// 소리공방
Route::prefix('workshop')->group(function () {
    Route::get('/', 'WorkshopController@index')->name('workshop.main');
    Route::post('/upload', 'WorkshopController@upload')->name('workshop.upload');
    Route::post('/cutter', 'WorkshopController@cutter')->name('workshop.cutter');
});

Route::resource('store', 'StoreController');

Route::post('store/addCart', 'ProductsController@addCart')->name('cart.add');

Route::get('chart', 'TimeTestController@chart');
