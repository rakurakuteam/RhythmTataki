<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class AddDlCheckToHeartsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::table('hearts', function (Blueprint $table) {
            $table->boolean('dl_check')->default(0)->comment('다운로드 체크')->after('heart');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('hearts', function (Blueprint $table) {
            $table->dropColumn('dl_check');
        });
    }
}
