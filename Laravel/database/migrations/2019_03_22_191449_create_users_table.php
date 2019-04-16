<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateUsersTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('users', function (Blueprint $table) {
            $table->increments('id')->comment('회원번호');
            $table->string('email', 50)->comment('이메일');
            $table->string('password', 255)->comment('비밀번호');
            $table->string('name', 30)->comment('회원이름');
            $table->string('phone', 20)->nullable()->comment('회원전화번호');
            $table->integer('point')->default(0)->comment('회원포인트');
            $table->boolean('confirm')->default(false)->comment('이메일인증');
            $table->string('token', 255)->nullable()->comment('토큰');
            $table->timestamp('token_exp')->nullable()->comment('토큰 만료일');
            $table->timestamps(); // 등록일, 수정일
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('users');
    }
}
