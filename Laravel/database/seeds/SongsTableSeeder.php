<?php

use Illuminate\Database\Seeder;

class SongsTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        $songs = [
            '올챙이와 개구리', '아기염소', '곰세마리', '솜사탕',
            '예쁜아기곰', '개구리송', '클로버', '초록바다',
            '엄마돼지 아기돼지', '퐁당퐁당', '뽀뽀뽀',
        ];

        for($i=0; $i<count($songs); $i++){
            DB::table('songs')->insert([
                'name' => $songs[$i],
            ]);
        }
    }
}
