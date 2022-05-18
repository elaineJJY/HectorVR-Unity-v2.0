import glob
import os
import pandas as pd
import matplotlib.pyplot as plt
import time
import numpy as np
import seaborn as sns
import math

path = os.getcwd()

def get_average(records):
    return sum(records) / len(records)


def get_variance(records):
    average = get_average(records)
    return sum([(x - average) ** 2 for x in records]) / len(records)


def get_standard_deviation(records):
    variance = get_variance(records)
    return math.sqrt(variance)


def get_rms(records):
    return math.sqrt(sum([x ** 2 for x in records]) / len(records))


def get_mse(records_real, records_predict):
    if len(records_real) == len(records_predict):
        return sum([(x - y) ** 2 for x, y in zip(records_real, records_predict)]) / len(records_real)
    else:
        return None


def get_rmse(records_real, records_predict):
    mse = get_mse(records_real, records_predict)
    if mse:
        return math.sqrt(mse)
    else:
        return None


def get_mae(records_real, records_predict):
    if len(records_real) == len(records_predict):
        return sum([abs(x - y) for x, y in zip(records_real, records_predict)]) / len(records_real)
    else:
        return None


def writeSDCSV(filename):
    file = pd.read_csv("Mean.csv")
    conditions = file['condition']
    
    dict = {}
    dict['conditon'] = conditions
    for scale in scales:
        temp = []
        for condition in conditions:
            col = df_merged.groupby('condition').get_group(condition)
            col = col[scale]
            temp.append(get_standard_deviation(col))
        dict[scale] = temp
    df = pd.DataFrame(dict) 
    df.to_csv(filename)

def draw(filename):
    conditions = file['condition']
    result = file[filename]
    plt.figure(figsize=(9, 6), dpi=100)
    plt.bar(conditions, result, width=0.35, color=colors,alpha=a)
    plt.title(filename,fontsize=20)
    plt.ylabel('score')
    plt.grid(alpha=0, linestyle=':')
    plt.savefig(filename + ".jpg", dpi=300)
    #plt.show()

def drawRobotPerformance():
    plt.figure(figsize=(20,8))
    conditions = file['condition']
    
    sd = pd.read_csv(SD)
    
    error_params=dict(elinewidth=1,ecolor='black',capsize=5)
    
    plt.suptitle("Robot Performance",fontsize=20)

    plt.subplot(221)
    plt.title("Collision",fontsize=15)
    std_err = sd["Collision"]
    plt.ylabel('Times')
    plt.bar(conditions, file["Collision"], width=0.35, color=colors,alpha=a,yerr=std_err,error_kw=error_params)
    
    plt.subplot(222)
    plt.title("Drive Distance",fontsize=15)
    std_err = sd["Drive Distance"]
    plt.ylabel('Distance(m)')
    plt.bar(conditions, file["Drive Distance"], width=0.35, color=colors,alpha=a,yerr=std_err,error_kw=error_params)
    
    plt.subplot(223)
    plt.title("Total driving time",fontsize=15)
    std_err = sd["Total driving time"]
    plt.ylabel('Time(s)')
    plt.bar(conditions, file["Total driving time"], width=0.35, color=colors,alpha=a,yerr=std_err,error_kw=error_params)

    plt.subplot(224)
    plt.title("Average speed",fontsize=15)
    std_err = sd["Adverage speed"]
    plt.ylabel('Speed(m/s)')
    plt.bar(conditions, file["Adverage speed"], width=0.35, color=colors,alpha=a,yerr=std_err,error_kw=error_params)
    
    plt.savefig("Robot Performance",dpi=300)

def drawRescue():
    plt.figure(figsize =(7,7))
    x = np.arange(len(scales))
    total_width, n = 0.8, 4
    width = total_width / n
    sd = pd.read_csv(SD)
    error_params=dict(elinewidth=1,ecolor='black',capsize=5)

    # set range
    plt.ylim(0, 10.5)

    for i in range(0,4):
        result = []
        std_err = []
        for scale in scales:
            result.append(file.iloc[i][scale])
            std_err.append(sd.iloc[i][scale])
        plt.bar(x+width*(i-1),result,width=width,color=colors[i],label=file.iloc[i]["condition"],alpha=a,yerr=std_err,error_kw=error_params)

    plt.legend()
    plt.title("Rescue situation",fontsize=15)
    plt.xticks(x+width/2,scales)
    plt.ylabel('Person')
    #plt.show()
    
    plt.savefig("Rescue situation",dpi=300)



# Merge all the .csv file 
all_files = glob.glob(os.path.join(path, "*.csv"))
df_from_each_file = (pd.read_csv(f, sep=',') for f in all_files)
df_merged = pd.concat(df_from_each_file, ignore_index=True)

# Save the file to Merged.csv in the same folder
# df_merged.to_csv( "Merged.csv")

# save the results in csv
df_merged["condition"] = df_merged["condition"].apply(lambda x: x.replace("Test",""))
file = df_merged.groupby(["condition"]).mean() 
file.to_csv( "Mean.csv")

scales = ["Collision","Drive Distance","Total driving time","Adverage speed","Rescued Target", "Remained Visible Target","Remained Unvisible Target"]
SD = "standard_deviation.csv"
writeSDCSV(SD)

file = pd.read_csv("Mean.csv")




colors = sns.color_palette()
a = 0.6


# for scale in scales:
#     draw(scale)

#scales = ["Rescued Target", "Remained Visible Target","Remained Unvisible Target"]
scales = ["Rescued Target", "Remained Visible Target"]
drawRescue()


scales = ["Collision","Drive Distance","Total driving time","Adverage speed"]
drawRobotPerformance()

os.remove("Mean.csv")
os.remove(SD)


