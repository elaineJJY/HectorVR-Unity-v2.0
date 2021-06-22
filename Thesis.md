# 更新点

1. ~~菜单可移动： wie oculus 里的菜单~~
2. 地图vs 雷达  Layer：利用collidor 更改layer  仅30m可看
3. 地图重新改一下  变小：每个场景相似但不同
4. camera 看不见 用户的（）也是layer
5. set target/follow 人：用假功能：自动寻路  NavMesh
6. 距离（）



Zeit：5 min akku

10 Taerget

4 Block

记录的点：

Distance：移动距离

Anzahl ：vorbei  aber nicht rescue

Collidor times

## Thesis

1. Länge: 20-45 Seite? (inhalt ist wichtiger  20 ist auch okay)
2. Latex Template(teilweise auf deutsch und Format)
3. Anmeldung & genaue Title
4. Zeitplan
5. Feedback/Korrektur/Hilfe für Thesis
6. Präsentation(heimatland)  ok



###### Content

1. ROS
	- weg machen? Oder nur über Position Synchronismus
2. emergenCITY? (1 satz)
3. Geliederung überprüfen



## Project

1. ROS (nur in Simulation Sccene, nicht in Test Scenes)
2. Camera creator nicht active(einfach weg machen oder)
3. Commentar/ Code Quality von Project(写一下)
4. Vorbereitung und Ablauf über Nutzerstudie
	+ Vorstellung Video? （list  ）
	+ Fragebogen
	+ wie viele Leute (woher kommen diese Leute)  4*n===>8-16
	+ machen wir zusammen oder （allein）



------



# Abstract



# Introduction

1.5 Seite

> ##### Rescue Robot
>
> - What is rescue robots & some use cases of rescue robots
> - Existed control methods of robots

In recent years, natural disasters such as earthquakes, tsunamis and potential nuclear, chemical, biological and explosives have seriously threatened the safety of human life and property. While the number of various disasters has increased, their severity, diversity and complexity have also gradually increased. The 72h after a disaster is the golden rescue time, but the unstructured environment of the disaster site makes it difficult for rescuers to work quickly, efficiently and safely.

Rescue robots have the advantages of high mobility and handling breaking capacity, can work continuously to improve the efficiency of search and rescue, and can achieve the detection of graph, sound, gas and temperature within the ruins by carrying a variety of sensors, etc.
Moreover, the robot rescue can assist or replace the rescuers to avoid the injuries caused by the secondary collapse and reduce the risk of rescuers. Therefore, rescue robots have become an important development direction.

In fact, rescue robots have been put to use in a number of disaster scenarios. The Center for Robot-Assisted Search and Rescue (CRASAR) used rescue robots for Urban Search and Rescue (USAR) task during the World Trade Center collapse in 2001 \cite{Casper:2003tk} and has employed rescue robots at multiple disaster sites in the years since to assist in finding survivors, inspecting buildings and scouting the site environment etc \cite{Murphy:2012th}. Anchor Diver III was utilized as underwater support to search for bodies drowned at sea after the 2011 Tohoku Earthquake and Tsunami \cite{Huang:2011wq}.

Considering the training time and space constraints for rescuers \cite{Murphy:2004wl}, and the goal of eﬃciency and ﬂuency collaboration \cite{10.1145/1228716.1228718}, the appropriate human-robot interaction approach deserves to be investigated. Some of the existing human-computer interaction methods are Android software \cite{Sarkar:2017tt} \cite{Faisal:2019uu}, gesture recognition\cite{Sousa:2017tn} \cite{10.1145/2157689.2157818} \cite{Nagi:2014vu}, facial voice recognition \cite{Pourmehr:2013ta}, adopting eye movements \cite{Ma:2015wu}, Augmented Reality(AR)\cite{SOARES20151656} and Virtual Reality(VR), etc.



> ##### VR and robot
>
> ###### What is VR
>
> 
>
> ###### VR Advantage
>
> - general advantages
>- advantage regarding robots
> 
>###### VR limatation and challenges
> 
> - disadvantages
>- challenges:  improve the level of human-computer integration.
> - There remains a need to ...

Among them, VR has gained a lot of attention due to its immersion and the interaction method that can be changed virtually. VR is reliable as a novel alternative to human-robot interaction. The interaction tasks that users can accomplish with VR devices do not differ significantly from those using real operating systems\cite{Villani:2018ub}. VR displays can provide users with stereo viewing cues, which makes collaborative human-robot interaction tasks in certain situations more efficient and performance better \cite{Liu:2017tw}.



> ##### What I have done (overview)
>
> ###### Unity Project
>
> - main goal
> - 4 modes
>
> - test scenes
>
> 
>
> ###### User Study
>
> - Testing process
>
> - General content of the survey



> ##### Paper Architecture



# (Related Work)



# Implementation

## 1. Overview

> - the purpose of the unity project
> - Components of the project: 4 operation modes & test Scene



## 2. System Architecture

> - Compurter Information: CPU,GPU
>
> - HTC Vive（半页）
>
> - ROS and Robot
>
> - Unity VR engine & SteamVR
>
> - [UML/ Relationship photo]
>
> 	



## 3. Interaction techniques



The Unity project offers 4 main modes of robot operation...

##### 1. Handle Mode

> - main feature
> - functions: how to move robot, camera, map...
> - photo



##### 2. Lab Mode

> - main feature
> - functions: how to move robot, button, speed editor, auto drive 3 monitor....
> - photo



##### 3. Remote Mode

> - main feature
> - functions: how to move robot: target(Pseudocode?) or virtural joystick. ItemPackage in Steam
> - photo



##### 4. UI Mode

> - main feature
> - functions: introcude here compositions of the UI menu
> - photo





## 4. Test Scene

> - goal of the project: rescue robots  => destroyed city,
>- environment:  destroyed city & Collider for test  [photo] 





# Evaluation of User Experience

> ##### main goal of test (Overview)
>
> - Evaluate user experience and robot performance in different operating modes



> ##### test process
>
> - test group
>
> - task: The user is required to .....
> - what will be recorded: time, collisions ...(?)



> ##### results and discussion
>
> - Difficulty of learning how to operate
> - Efficiency of operating robots (time)
> - Number of robot collisions (collision)
> - [Table: Test results]





# Conclusion

> ##### What I have done (overview)
>
> ###### Unity Project
>
> - 4 operation modes
>
> - test scenes
>
> 
>
> ###### User Study
>
> - compared and evaluated .....
> - results:  .......



> ##### Future work
>
> - communication with ROS
> - Real Robots
> - Use real scene: reconstructed 3D model based on the daten from robot sensors