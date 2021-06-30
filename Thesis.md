# Abstract



# Introduction

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

Among them, VR has gained a lot of attention due to its immersion and the interaction method that can be changed virtually. VR is no longer a new word. With the development of technology in recent years, VR devices are gradually becoming more accessible to users. With the improvement of hardware devices, the new generation of VR headsets have higher resolution and wider field of view. And in terms of handle positioning, with the development of computer vision in the past few years, VR devices can now use only the four cameras mounted on the VR headset to achieve accurate spatial positioning, and support hand tracking, accurately capturing every movement of hand joints. While VR are often considered entertainment devices, VR brings more than that. It plays an important role in many fields such as entertainment, training, education and medical care.

The use of VR in human-computer collaboration also has the potential. In terms of reliability, VR is reliable as a novel alternative to human-robot interaction. The interaction tasks that users can accomplish with VR devices do not differ significantly from those using real operating systems\cite{Villani:2018ub}. In terms of user experience and operational efficiency, VR displays can provide users with stereo viewing cues, which makes collaborative human-robot interaction tasks in certain situations more efficient and performance better \cite{Liu:2017tw}.

However, there remains a need to explore human-computer interaction patterns and improve the level of human-computer integration\cite{Wang:2017uy}. Intuitive and easy-to-use interaction methods can enable the user to explore the environment as intentionally as possible and improve the efficiency of search and rescue.



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

For this purpose, this paper presents a preliminary VR-based system for the simulation of ground rescue robots with four different modes of operation and corresponding test scenes imitating a post-disaster city. The test scene simulates a robot collaborating with Unity to construct a virtual 3D scene. The robot has a simulated radar, which makes the display of the scene dependent on the robot's movement. In order to find a control method that is as intuitive and low mental fatigue as possible, a user survey was executed after the development was completed.



> ##### Paper Architecture

Section \ref{*i*mplementation} provides details of the purposed system, including the techniques used for the different interaction modes and the structure of the test scenes.
Section \ref{evaluate} will talk about the design and process of user study.

Section \ref{result} presents the results of the user study and analyzes the advantages and disadvantages of the different modes of operation and the directions for improvement.

Finally, in Section \ref{conclusion}, conclusions and future work are summarized.



# (Related Work)



# Implementation

% summary

In this chapter, the tools and techniques used in building this human-computer collaborative VR-based system are described. The focus will be on interaction techniques for different modes of operation. In addition, the setup of the robot and the construction of test scenes will also be covered in this chapter.



## 1. Overview

> - the purpose of the unity project
> - Components of the project: 4 operation modes & test Scene

The main goal of this work is to design and implement a VR-based human-robot collaboration system with different methods of operating the robot in order to find out which method of operation is more suitable to be used to control the rescue robot. Further, it is to provide some basic insights for future development directions and to provide a general direction for finding an intuitive, easy-to-use and efficient operation method. Therefore, the proposed system was developed using Unity, including four modes of operation and a corresponding test environment for simulating post-disaster scenarios. In each operation mode, the user has a different method to control the robot. In addition, in order to better simulate the process by which the robot scans its surroundings and the computer side cumulatively gets a reconstructed 3D virtual scene, the test environment was implemented in such a way that the picture seen by the user depends on the direction of the robot's movement and the trajectory it travels through.



## 2. System Architecture

> - Compurter Information: CPU,GPU
>
> - HTC Vive
>
> - ROS and Robot
>
> - Unity VR engine & SteamVR
>
> 

The proposed system runs on a computer with the Windows 10 operating system. This computer has been equipped with an Intel Core i7-8700K CPU, 32 GB RAM as well as a NVIDIA GTX 1080 GPU with 8 GB VRAM. HTC Vive is used as a VR device. It has a resolution of 1080 × 1200 per eye, resulting in a total resolution of 2160 × 1200 pixels, a refresh rate of 90 Hz, and a field of view of 110 degrees. It includes two motion controllers and uses two Lighthouses to track the position of the headset as well as the motion controllers.

Unity was chosen as the platform to develop the system. Unity is a widely used game engine with a Steam VR plugin \footnote{https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647}, which allows developers to focus on the VR environment and interactive behaviors in programming, rather than specific controller buttons and headset positioning, making VR development much simpler. Another reason why Unity was chosen as a development platform was the potential for collaboration with the Robot Operating System (ROS), a frequently used operating system for robot simulation and manipulation, which is flexible, low-coupling, distributed, open source, and has a powerful and rich third-party feature set. In terms of collaboration between Unity and ROS, Siemens provides open source software libraries and tools in C\# for communicating with ROS from .NET applications \footnote{https://github.com/siemens/ros-sharp}. Combining ROS and Unity to develop a collaborative human-robot interaction platform proved to be feasible\cite{Whitney:2018wk}. Since the focus of this paper is on human-robot interaction, collaboration and synchronization of ROS will not be explored in detail here.



## 3. Robot

> camera
>
> radar
>
> layer change => collider
>
> information

To simulate the process of a robot using a probe camera to detect the real environment and synchronise it to Unity, a conical collision body was set up on the robot. The robot will transform the Layers of the objects in the scene into visible Layers by collision detection as it is driving. In addition, the robot's driving performance, such as the number of collisions, average speed, total distance, etc., will be recorded in each test. The detailed recorded information can be seen in Fig.\ref{fig:uml}. The movement of the robot depends on the value of the signal that is updated in each mode. In addition, the robot's Gameobject has the NavMeshAgent \footnote{https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.html} component, which supports the robot's navigation to the specified destination with automatic obstacle avoidance in the test scene.



## 4. Interaction techniques

This system has 4 different approaches to control the robot. Each mode has its own distinctive features: 

```latex
\begin{enumerate}
\item In Handle Mode the user will send control commands directly using the motion controller. 
\item In Lab Mode a simulated lab is constructed in the VR environment and the user will use virtual buttons in the lab to control the rescue robot. 
\item In Remote Mode the user can set the driving destination directly. 
\item In UI Mode the user has a virtual menu and sends commands via rays from the motion controller.
\end{enumerate}
```

In order to improve the reusability of the code and to facilitate the management of subsequent development, the classes that manage the interaction actions of each mode implement the same interface. A graphical representation of the system structure is given in the UML activity diagram in Fig.\ref{fig:uml}.

```latex
\begin{figure}[h]
    \centering
    \includegraphics[height=12cm]{graphics/uml.png}
    \caption{UML Class diagram for the main structure of the system}
    \label{fig:uml}
\end{figure}
```



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





## 5. Test Scene

> - goal of the project: rescue robots  => destroyed city,
> - environment:  destroyed city & Collider for test  [photo] 
> - radar layer

In order to simulate the use of rescue robots in disaster scenarios, the test scenes were built to mimic the post-disaster urban environment as much as possible. The POLYGON Apocalypse\footnote{https://assetstore.unity.com/packages/3d/environments/urban/polygon-apocalypse-low-poly-3d-art-by-synty-154193}, available on the Unity Asset Store, is a low poly asset pack with a large number of models of buildings, streets, vehicles, etc. Using this resource pack as a base, additional collision bodies of the appropriate size were manually added to each building and obstacle after the pack was imported, which was needed to help track the robot's driving crash in subsequent tests.

Considering that there are four modes of operation to be tested, four scenes with similar complexity, similar composition of buildings but different road conditions and placement of buildings were constructed. The similarity in complexity of the scenes ensures that the difficulty of the four tests is basically identical. The different scene setups ensure that the scene information learned by the user after one test will not make him understand the next test scene and thus affect the accuracy of the test data. 

The entire scene is initially invisible, and the visibility of each objects in the test scene is gradually updated as the robot drives along. Ten interactable sufferer characters were placed in each test scene. The place of placement can be next to the car, the house side and some other reasonable places.



# Evaluation of User Experience

> ##### main goal of test (Overview)
>
> - Evaluate user experience and robot performance in different operating modes

This chapter describes the design and detailed process of the user evaluation. The purpose of this user study is to measure the impact of four different modes of operation on rescue efficiency, robot driving performance, and psychological and physiological stress and fatigue, etc. For this purpose, participants are asked to find victims in a test scene using different modes of operation and to answer a questionnaire after the test corresponding to each mode of operation.



## Study Design

The evaluation for each mode of operation consists of two main parts. The first part is the data recorded during the process of the participant driving the robot in the VR environment to find the victims. The recorded data includes information about the robot's collision and the speed of driving etc. The rescue of the victims was also considered as part of the evaluation. Besides the number of victims rescued, the number of victims who were already visible but neglected is also important data. The Official NASA Task Load Index (TLX) was used to measure the participants subjective workload asessments. Additionally, participants were asked specific questions for each mode of operation and were asked to select their favorite and least favorite modes of operation.



## Procedure

##### Demographics and Introduction 

> 1. inform the purpose and collected data
> 2. basic demographics(google form)
> 3. introduce 4 mode: verbal + show motion controller

Before the beginning of the actual testing process, participants were informed of the purpose of the project, the broad process and the content of the data that would be collected. After filling in the basic demographics, the features of each of the four modes of operation and their rough usage were introduced verbally with a display of the buttons on the motion controllers.



##### Entering the world of VR

> 1. wear the headset
> 2. familiar with the menu : switch & select mode(practice)
> 3. change position : teleport & raise or lower
> 4. rescue 1 victim

After the basic introduction part, participants would directly put on the VR headset and enter the VR environment to complete the rest of the tutorial. Considering that participants might not have experience with VR and that it would take time to learn how to operate the four different modes, the proposed system additionally sets up a practice pattern and places some models of victims in the practice scene. After entering the VR world, participants first needed to familiarize themselves with the opening and selecting options of the menu, as this involves switching between different modes and entering the test scenes. Then participants would use the motion controllers to try to teleport themselves, or raise themselves into mid-air. Finally participants were asked to interact with the victim model through virtual hands. After this series of general tutorials, participants were already generally familiar with the use of VR and how to move around in the VR world.



##### Practice and evaluation of patterns

> 1. `foreach Mode`:
>	1. enter mode(practice)
> 	2. try to move the robot
> 	3. try to rescue 1-2 victims
> 	4. enter test scene
> 	5. -testing- 
> 	6. Fill out the questionnaire: google form + TLX
> 
> 2. in the end: summary part of google form: like/dislike most. + reason + feedback

Given the different manipulation approaches for each mode, in order to avoid confusion between the different modes, participants would then take turns practicing and directly evaluating each mode immediately afterwards. The participant first switched to the mode of operation to be tested and manipulated the robot to move in that mode. After attempting to rescue 1-2 victim models and the participant indicated that he or she was familiar enough with this operation mode, the participant would enter the test scene. In the test scene, participants had to save as many victims as possible in a given time limit. Participants were required to move the robot around the test scene to explore the post-disaster city and to find and rescue victims. In this process, if the robot crashes with buildings, obstacles, etc., besides the collision information being recorded as test data, participants would also receive sound and vibration feedback. The test will automatically end when time runs out or when all the victims in the scene have been rescued. Participants were required to complete the evaluation questionnaire and the NASA evaluation form at the end of each test. This process was repeated in each mode of operation. 

After all the tests were completed, participants were asked to compare the four operation modes and select the one they liked the most and the one they liked the least. In addition, participants could give their reasons for the choice and express their opinions as much as they wanted, such as suggestions for improvement or problems found during operation.



# Results

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