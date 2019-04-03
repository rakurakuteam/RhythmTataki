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

Route::get('/', function(){
    return view('master');
});

// 구글 로그인
Route::prefix('login')->group(function () {
    Route::get('/google', 'GoogleLoginController@googleLogin')->name('google.login');
    Route::get('/google/callback', 'GoogleLoginController@googleCallback')->name('google.callback');
});

Auth::routes();

Route::get('/home', 'HomeController@index')->name('home');
Route::get('/board/{id}', 'HomeController@show')->name('board');
Route::get('/myPage', 'HomeController@myPage')->name('myPage');
Route::get('/userPage/{id}', 'HomeController@userPage')->name('userPage');

// 소리공방
Route::prefix('workshop')->group(function () {
    Route::get('/', 'WorkshopController@index')->name('workshop.main');
    Route::post('/upload', 'WorkshopController@upload')->name('workshop.upload');
    Route::post('/cutter', 'WorkshopController@cutter')->name('workshop.cutter');
});

Route::resource('product', 'ProductController');