   M S B A M L   `   `   `  ����>  :Nba, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:NbaNba  5          Nba.TeamDetail   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      #localclr-namespace:Nba     5      -   5          N� Title$  
TeamDetail��5	       � 	Resources 5
       
  Search �� Text  Nba.TeamLogoConverter  TeamLogoConverter   Nba.StatusConverter  StatusConverter  teamViewSource  seasonViewSource  playerInTeamViewSource  matchupViewSource��%�   &	        &	       &	 3     &	 F     &	 Y     &	 l     &	      �� 5   
   $ Search��6)     5   
   6"     5   
   6    �� 5   
   6   �� 5   
   6   �� 5   
   6   �� 5   
   6   5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command# [  5      6;   .��5      � .+�5   
   � $��64��6   $��0q�6   $	��Top=�6&    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#   �� 5      $
��Auto �6#    � RowDefinitions 5      �� 5       $
��Auto �6    .+�5"      �� $��0��6   $
��Left=�6$   $��64��6?   $��16,3,3,3q�6K   $��Center=�6]   $��37��6x   $��img/logo.jpg��6�   �� 5#      $��NBA Management System��6   $��1��64   $
��Left=�6D   $��3q�6_   $��Center=�6j     
HeaderText#��[ 6�   �� 5$      $��Team Detail��6   $��2��6*   $��3q�6:   $��Center=�6E   $��Center=�6b   #��[ 6}   �� 5%      -   5        $
��Back��5%      $��3��6$   $
��Left=�64   $��3,3,16,3q�6O   $��Center=�6a   $��75��6|   6�   5&      �� 5'   
   $	��100��6T   $��
16,80,16,0q�6a   $	��Top=�6u    �� Orientation$ 
Horizontal=�6�    � DataContext 6   �� 	 �� Source#	 [ .+�5(      �� $��96��6Z   $��96��6f   ��6   �� *Logo+
 �� 	Converter#
 [ 6f   �� 5)      #��[ 61   $��Center=�6U   $��Center=�6r   $��8,0,0,0q�6�   ��6   �� *
TeamName+6�   �� 5*      $��|��6   #��[ 6    $��Center=�6D   $��Center=�6a   $��8,0,0,0q�6|   �� 5+      #��[ 66   $��Center=�6Z   $��Center=�6w   $��8,0,0,0q�6�   ��6   �� *Division.Name+6�   �� 5,      $��of��6   #��[ 6!   $��Center=�6E   $��Center=�6b   $��4,0,0,0q�6}   �� 5-      #��[ 6A   $��Center=�6e   $��Center=�6�   $��4,0,0,0q�6�   ��6   �� *Division.Conference.Name+6�   �� 5.      -   5         ��Name$ seasonComboBox��5.       �� DisplayMemberPath$
 Name��6/   $��16,0,0,0q�6�    �� SelectedValuePath$
 Name��6�   $	��177��6�   $��Center=�6�   $��Center=�6�   ��6H   �� #	 [ 6�   �� 5/      $��Search��6   #��[  6&   $	��100��6�   $��Center=�6�   $��48��6�   $��16,0,0,0q�6�   ��6H   �� *
SeasonId+#	 [ 6�   50      �� 51   
   -   5         ��Name$ 
tabControl��51      $��	8,196,8,8q�6)   .@�52      �� $��Roster��6   .��53      � ���	#FFE5E5E5.+�54      &   System.Windows.Controls.DataGrid  -   5          Name$ playerInTeamsDataGrid��54         AutoGenerateColumns .     EnableRowVirtualization . $��8q�6�      RowDetailsVisibilityMode$ VisibleWhenSelected=�6�      
IsReadOnly . ��6y   �� #	 [    Columns 55      0  *System.Windows.Controls.DataGridTextColumn  56      -   5        numberColumn��,  &System.Windows.Controls.DataGridColumn   Header$	 No.��56   g      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6t   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   �� *ShirtNumber+6t     57      -   5        playerNameColumn��$
 Name��57   k   $
 Auto 6y    6K   �� *Player.Name+6y     58      -	   5        positionColumn��$ Position��58   r   $
 Auto 6�    6I   �� *Player.Position.Abbr+6�     59      -
   5        dateOfBirthColumn��$ Date of Birth��59   �   $
 Auto 6�    6L   �� *Player.DateOfBirth+ �� StringFormat$ {0:yyyy-MM-dd}��6�     5:      -   5        collegeColumn��$ College��5:   k   $
 Auto 6|    6H   �� *Player.College+6|     5;      -   5        salaryColumn��$ Salary��5;   b   $
 Auto 6r    6G   �� *Salary+6r   5=      5>      5?      �� 5@      $��Matchup��6   .��5A      � ���	#FFE5E5E5# [ 6.   .+�5B        -   5        $ matchupDataGrid��5B       .   . $��8q�6�   $ VisibleWhenSelected=�6�    . ��6s   ��  5C        5D      -   5        	dayColumn��$
 Date��5D   �   $
 Auto 6�    6D   �� *	Starttime+$ {0:yyyy-MM-dd}��6�     5E      -   5        matchupTypeColumn��$ Matchup Type��5E   q   $
 Auto 6�    6L   �� *MatchupType.Name+6�     5F      -   5        locationColumn��$ Location��5F   f   $
 Auto 6x    6I   �� *
Location+6x     5G      -   5        teamHomeScoreColumn��$ Team Home Score��5G   r   $
 Auto 6�    6N   �� *Team_Home_Score+6�     5H      -   5        teamAwayScoreColumn��$ Team Away Score��5H   r   $
 Auto 6�    6N   �� *Team_Away_Score+6�     5I      -   5        
timeColumn��$ 
Start Time��5I   }   $
 Auto 6�    6E   �� *	Starttime+$ 	{0:HH:mm}��6�     5J      -   5        statusColumn��$ Status��5J   �   $
 Auto 6�    6G   �� *Status+#
 [ 6�   5L      5M      5N      �� 5O      $��Lineup��6   .��5P      � ���	#FFE5E5E5.+�5Q      �� $��8q�6   $��img/lineup.jpg��6'   �� 5R      $ 
Horizontal=�6!   $
��Left=�6:   $	��Top=�6U   $��150,100,0,0q�6m   .+�5S      �� $��PF��6    $
��Left=�6-   $	��Top=�6H   ���	#FFDDD122���	#FF0B2DBF$��18(�6�   �� 5T      -   5         ��Name$ pfTextBlock��5T   $   $
��Left=�69   $��WrapWithOverflow=�6T   $��	TextBlock��6t   $	��Top=�6�   ���	#FFE8EDE8���	#FF0A19C5$��14(�6�   5U      �� 5W      $ 
Horizontal=�6!   $��Center=�6:   $	��Top=�6W   $��0,50,0,0q�6o   .+�5X      �� $��C��6    $
��Left=�6,   $	��Top=�6G   ���	#FFDDD122���	#FF0B2DBF$��18(�6�   �� 5Y      -   5        $ 
cTextBlock��5Y   $   $
��Left=�68   $��WrapWithOverflow=�6S   $��	TextBlock��6s   $	��Top=�6�   ���	#FFE8EDE8���	#FF0A19C5$��14(�6�   5Z      �� 5\      $ 
Horizontal=�6!   $��Right=�6:   $	��Top=�6V   $��0,100,100,0q�6n   .+�5]      �� $��SF��6    $
��Left=�6-   $	��Top=�6H   ���	#FFDDD122���	#FF0B2DBF$��18(�6�   �� 5^      -   5        $ sfTextBlock��5^   $   $
��Left=�69   $��WrapWithOverflow=�6T   $��	TextBlock��6t   $	��Top=�6�   ���	#FFE8EDE8���	#FF0A19C5$��14(�6�   5_      �� 5a      $ 
Horizontal=�6!   $
��Left=�6:   $��Bottom=�6U   $��
200,0,0,50q�6p   .+�5b      �� $��SG��6    $
��Left=�6-   $	��Top=�6H   ���	#FFDDD122���	#FF0B2DBF$��18(�6�   �� 5c      -   5        $ sgTextBlock��5c   $   $
��Left=�69   $��WrapWithOverflow=�6T   $��	TextBlock��6t   $	��Top=�6�   ���	#FFE8EDE8���	#FF0A19C5$��14(�6�   5d      �� 5f      $ 
Horizontal=�6!   $��Right=�6:   $��Bottom=�6V   $��
0,0,200,50q�6q   .+�5g      �� $��PG��6    $
��Left=�6-   $	��Top=�6H   ���	#FFDDD122���	#FF0B2DBF$��18(�6�   �� 5h      -   5        $ pgTextBlock��5h   $   $
��Left=�69   $��WrapWithOverflow=�6T   $��	TextBlock��6t   $	��Top=�6�   ���	#FFE8EDE8���	#FF0A19C5$��14(�6�   5i      5j      5k      5l      5m      5n      