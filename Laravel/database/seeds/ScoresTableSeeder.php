<?php

use Illuminate\Database\Seeder;

class ScoresTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run(){
        for($i=1; $i<=10; $i++){
            DB::table('scores')->insert([
                'user_id' => random_int(1, DB::table('users')->count()),
                'song' => '',
                'score' => random_int(50000, 999999),
                'created_at' => now()
            ]);
        }
    }
}
