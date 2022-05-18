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



def draw(scale):
    conditions = file['condition']

    result = file[scale]
    plt.figure(figsize=(9, 6), dpi=100)

    sd = pd.read_csv(SD)
    std_err = sd[scale]
    error_params=dict(elinewidth=1,ecolor='black',capsize=5)
    plt.bar(conditions, result, width=0.35, color=colors,alpha=a,yerr=std_err,error_kw=error_params)

    plt.title(scale,fontsize=15)
    plt.ylabel('score')
    plt.grid(alpha=0, linestyle=':')
    plt.savefig(scale, dpi=300)
    #plt.show()

def drawTogether():
    scales = ["mental-demand","physical-demand","temporal-demand","performance", "effort","frustration","total"]
    scales = ["total","mental-demand","physical-demand","frustration"]
    plt.figure(figsize=(15,7))
    x = np.arange(len(scales))
    total_width, n = 0.8, 4
    width = total_width / n
    
    for i in range(0,4):
        result = []
        std_err = []
        sd = pd.read_csv(SD)
        for scale in scales:
            result.append(file.iloc[i][scale])
            std_err.append(sd.iloc[i][scale])
        error_params=dict(elinewidth=1,ecolor='black',capsize=5)
        plt.bar(x+width*(i-1),result,width=width,color=colors[i],label=file.iloc[i]["condition"],alpha=a,yerr=std_err,error_kw=error_params)

    plt.legend()
    # plt.title("TLX Average",fontsize=15)
    plt.xticks(x+width/2,scales)
    #plt.show()
    plt.ylabel('score')
    plt.savefig("summary.jpg",dpi=300)




# Merge all the .csv file start with "HectorVR", and 
all_files = glob.glob(os.path.join(path, "HectorVR*.csv"))
df_from_each_file = (pd.read_csv(f, sep=',') for f in all_files)
df_merged = pd.concat(df_from_each_file, ignore_index=True)

# Save the file to Merged.csv in the same folder
df_merged.to_csv( "Merged.csv")

# save the results in csv
file = df_merged.groupby(["condition"]).mean() 
file.to_csv("Mean.csv")
scales = ["mental-demand","physical-demand","temporal-demand","performance", "effort","frustration","total"]
SD = "standard_deviation.csv"
writeSDCSV(SD)

        
file = pd.read_csv("Mean.csv")
colors = sns.color_palette()
a = 0.6
for scale in scales:
    draw(scale)

drawTogether()


