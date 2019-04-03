<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateBoardsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('boards', function (Blueprint $table) {
            $table->increments('id')->comment('게시글번호');
            $table->unsignedInteger('user_id')->commetn('회원번호');
            $table->foreign('user_id')->references('id')->on('users')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->string('title', 100)->comment('제목');
            $table->text('content')->comment('내용');
            $table->unsignedInteger('hits')->default(0)->comment('조회수');
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('boards', function(Blueprint $table){
            $table->dropForeign('boards_user_id_foreign');
        });
        Schema::dropIfExists('boards');
    }
}
