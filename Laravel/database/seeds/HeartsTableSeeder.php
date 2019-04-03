<?php

use Illuminate\Database\Seeder;

class HeartsTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        for($i=1; $i<=20; $i++){
            DB::table('hearts')->insert([
                'board_id' => random_int(1, DB::table('boards')->count()),
                'user_id' => random_int(1, DB::table('users')->count()),
                'heart' => random_int(0, 1),
                'created_at' => now(),
            ]);
        }
    }
}
