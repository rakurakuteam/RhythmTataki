<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateScoresTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('scores', function (Blueprint $table) {
            $table->increments('id');
            $table->unsignedInteger('user_id')->comment('회원번호');
            $table->foreign('user_id')->references('id')->on('users')
                    ->onUpdate('cascade')->onUpdate('cascade');
            $table->string('song', 255)->comment('노래제목');
            $table->unsignedBigInteger('score')->comment('점수');
            $table->timestamp('created_at');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('scores', function(Blueprint $table){
            $table->dropForeign('scores_user_id_foreign');
        });
        Schema::dropIfExists('scores');
    }
}
