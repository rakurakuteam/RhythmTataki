<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateHeartsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('hearts', function (Blueprint $table) {
            $table->increments('id');
            $table->unsignedInteger('board_id')->comment('게시판번호');
            $table->foreign('board_id')->references('id')->on('boards')
                    ->onUpdate('cascade')->onDelete('cascade');
            $table->unsignedInteger('user_id')->comment('회원번호');
            $table->foreign('user_id')->references('id')->on('users')
            ->onUpdate('cascade')->onDelete('cascade');
            $table->boolean('heart')->default(false)->comment('좋아요');
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
        Schema::table('hearts', function(Blueprint $table){
            $table->dropForeign('hearts_board_id_foreign');
            $table->dropForeign('hearts_user_id_foreign');
        });
        Schema::dropIfExists('hearts');
    }
}
